using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Models;

namespace LobbyingMadeSimple.Tests.Models
{

/// <summary>
/// Tests functionality of the Issue Model including logic to set approval and disapproval ratings
/// and determine if an Issue can be approved for funding by voting alone
/// </summary>
[TestClass]
    public class IssueTest
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Issue_constructor_defaults_are_valid()
        {
            Issue issue = new Issue();

            Assert.AreEqual(0, issue.UpvoteCount);
            Assert.AreEqual(0, issue.DownVoteCount);
            Assert.AreEqual(1500, issue.VoteCountNeeded);
            Assert.IsFalse(issue.IsApproved);
        }

        [TestMethod]
        public void Issues_can_be_up_voted()
        {
            Issue issue = new Issue();

            issue.AddVote(true);

            Assert.AreEqual(1, issue.UpvoteCount);
            Assert.AreEqual(0, issue.DownVoteCount);
        }

        [TestMethod]
        public void Issues_can_be_down_voted()
        {
            Issue issue = new Issue();

            issue.AddVote(false);

            Assert.AreEqual(1, issue.DownVoteCount);
            Assert.AreEqual(0, issue.UpvoteCount);
        }

        [TestMethod]
        public void Issues_can_be_approved_through_voting()
        {
            Issue issue = new Issue()
            {
                UpvoteCount = 1499
            };

            issue.AddVote(true);

            Assert.IsTrue(issue.IsApproved);
        }

        [TestMethod]
        public void Issues_can_be_denied_through_voting()
        {
            Issue issue = new Issue()
            {
                DownVoteCount = 1499
            };

            issue.AddVote(false);

            Assert.IsFalse(issue.IsApproved);
        }

        [TestMethod]
        public void Issues_are_rejected_if_less_than_67_percent()
        {
            Issue issue = new Issue()
            {
                UpvoteCount = 999,
                DownVoteCount = 500
            };

            issue.AddVote(false);

            Assert.IsFalse(issue.IsApproved);
        }

        [TestMethod]
        public void Issues_are_accepted_if_greater_than_67_percent()
        {
            Issue issue = new Issue()
            {
                UpvoteCount = 999,
                DownVoteCount = 500
            };

            issue.AddVote(true);

            Assert.IsTrue(issue.IsApproved);
        }

        [TestMethod]
        public void Issue_TotalVotes_shows_correct_count_when_voted()
        {
            // arrange
            Issue issue = new Issue()
            {
                UpvoteCount = 5,
                DownVoteCount = 5
            };

            Assert.AreEqual(10, issue.TotalVotes());
        }

        [TestMethod]
        public void Issue_NetScore_shows_correct_value()
        {
            Issue issue = new Issue()
            {
                UpvoteCount = 5,
                DownVoteCount = 3
            };

            Assert.AreEqual(2, issue.NetScore());
        }

        [TestMethod]
        public void Issue_VotesLeftUntilApproved_returns_correct_amount()
        {
            Issue issue = new Issue()
            {
                UpvoteCount = 400,
                DownVoteCount = 100
            };

            Assert.AreEqual(1000, issue.VotesLeftUntilApproval());
        }
    }
}
