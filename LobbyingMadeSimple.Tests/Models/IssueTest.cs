using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Models;
using Moq;
using Core;

namespace LobbyingMadeSimple.Tests.Models
{

/// <summary>
/// Tests functionality of the Issue Model including logic to set approval and disapproval ratings
/// and determine if an Issue can be approved for funding by voting alone
/// </summary>
[TestClass]
    public class IssueTest
    {
        private Vote upVote;
        private Vote downVote;
        private Issue issue;

        [TestInitialize]
        public void TestInit()
        {
            PropertyInit();
        }

        private void PropertyInit()
        {
            issue = new Issue();
            upVote = Mock.Of<Vote>(v => v.IsUpvote == true);
            downVote =  Mock.Of<Vote>(v => v.IsUpvote == false);
        }

        /// <summary>
        /// Tests the constructor defaults of the issue
        /// Any new construtor defaults should get Asserted here
        /// </summary>
        [TestMethod]
        public void Issue_constructor_defaults_are_valid()
        {
            // Assert
            Assert.AreEqual(1500, issue.VoteCountNeeded);
            Assert.IsTrue(issue.IsVotableIssue);
            Assert.AreEqual(50000.00, issue.FundingGoal);
            Assert.AreEqual(0, issue.FundingRaised);
        }

        [TestMethod]
        public void Issues_can_have_up_votes()
        {
            // Act
            issue.Votes = new List<Vote>()
            {
                upVote, upVote, upVote
            };

            // Assert
            Assert.AreEqual(3, issue.UpvoteCount);
            Assert.AreEqual(0, issue.DownVoteCount);
        }

        [TestMethod]
        public void Issues_can_be_down_voted()
        {
            // Act
            issue.Votes = new List<Vote>()
            {
                downVote, downVote, downVote
            };
            
            // Assert
            Assert.AreEqual(3, issue.DownVoteCount);
            Assert.AreEqual(0, issue.UpvoteCount);
        }

        [TestMethod]
        public void Issues_voting_is_turned_off_when_enough_votes_are_cast()
        {
            // Arrange
            issue.VoteCountNeeded = 6;

            // Act
            issue.Votes = new List<Vote>()
            {
                upVote, upVote, upVote, upVote, downVote, downVote
            };

            // Assert
            Assert.IsFalse(issue.IsVotableIssue);
        }

        [TestMethod]
        public void Issues_can_be_approved_through_voting()
        {
            // Arrange
            issue.VoteCountNeeded = 6;

            // Act
            issue.Votes = new List<Vote>()
            {
                upVote, upVote, upVote, upVote, downVote, downVote
            };

            // Assert
            Assert.IsTrue(issue.HasBeenApproved());
        }

        [TestMethod]
        public void Issues_can_be_denied_through_voting()
        {
            // Arrange
            issue.VoteCountNeeded = 6;

            // Act
            issue.Votes = new List<Vote>()
            {
                upVote, upVote, upVote, downVote, downVote, downVote
            };

            // Assert
            Assert.IsTrue(issue.HasBeenDenied());
        }

        [TestMethod]
        public void Issue_TotalVotes_shows_correct_count_when_voted()
        {
            // arrange
            issue.Votes = new List<Vote>()
            {
                upVote, upVote, upVote, downVote, downVote
            };

            // Assert
            Assert.AreEqual(5, issue.TotalVotes());
        }

        [TestMethod]
        public void Issue_VotesLeftUntilApproved_returns_correct_amount()
        {
            // Act
            issue.Votes = new List<Vote>()
            {
                upVote, upVote, upVote, upVote
            };

            // Assert
            Assert.AreEqual(1496, issue.VotesLeftUntilApproval());
        }

        [TestMethod]
        public void Issue_GetPercentage_shows_correct_value_rounded_to_2_decimals()
        {
            // Arrange
            issue.Votes = new List<Vote>()
            {
                upVote, upVote, upVote, upVote,
                downVote, downVote
            };

            // Act 
            string percent = issue.GetPrettyPercentage();

            // Assert
            Assert.AreEqual("67", percent);
        }

        [TestMethod]
        public void Issue_GetPercentage_returns_0_when_no_votes_are_cast()
        {
            // Arrange
            issue.Votes = new List<Vote>();

            //  Act
            string percent = issue.GetPrettyPercentage();

            // Assert
            Assert.AreEqual("0", percent);
        }

        [TestMethod]
        public void Issue_GetVoteForUser_returns_one_vote_if_it_exists_for_userId()
        {
            // Arrange
            var uId = "test";
            upVote.AuthorID = uId;
            issue.Votes = new List<Vote>()
            {
                upVote
            };

            // Act
            Vote result = issue.GetVoteForUser(uId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(uId, result.AuthorID);
        }

        [TestMethod]
        public void Issue_GetVoteForUser_returns_null_if_no_votes_exist()
        {
            // Arrange
            var uId = "test";
            issue.Votes = new List<Vote>();

            // Act
            Vote result = issue.GetVoteForUser(uId);

            // Assert
            Assert.IsNull(result);
        }
    }
}
