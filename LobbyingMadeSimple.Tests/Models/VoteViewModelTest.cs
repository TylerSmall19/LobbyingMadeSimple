using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using LobbyingMadeSimple.Core;
using Moq;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class VoteViewModelTest
    {
        [TestMethod]
        public void Has_readable_and_settable_properties()
        {
            // Act
            var vm = new VoteViewModel()
            {
                Id = 1,
                Title = "Title",
                ApprovalPercentage = "60",
                ShortDescription = "Short",
                ApprovalPercentageColor = "text-success",
                VoteCount = 1460,
                UpvoteButtonColor = "btn-success",
                DownvoteButtonColor = "btn-danger",
            };

            // Assert
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("60", vm.ApprovalPercentage);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("text-success", vm.ApprovalPercentageColor);
            Assert.AreEqual(1460, vm.VoteCount);
            Assert.AreEqual("btn-success", vm.UpvoteButtonColor);
            Assert.AreEqual("btn-danger", vm.DownvoteButtonColor);
        }

        [TestMethod]
        public void Can_be_converted_explicitly_from_Issue_entity()
        {
            // Arrange
            Issue issueEntity = Mock.Of<Issue>(
                i => i.Title == "Title"
                && i.ShortDescription == "Short"
                && i.LongDescription == "Long"
                && i.IsStateIssue == false
                && i.FundingGoal == 75000.00
                && i.CreatedAt == DateTime.Now
                && i.UpdatedAt == DateTime.Now
                && i.Id == 1
                && i.AuthorID == "authorId"
                && i.FundingRaised == 2500
                && i.IsFundable == true
                && i.IsVotableIssue == false
                && i.VoteCountNeeded == 0
                && i.Votes == new List<Vote>()
                && i.Author.Id == "AuthorID"
                && i.GetPrettyPercentage() == "60"
                && i.VotesLeftUntilApproval() == 1500
                && i.TotalVotes() == 40
                && i.GetVoteForUser("TestUid") == Mock.Of<Vote>(v => v.IsUpvote == true)
            );

            // Act
            VoteViewModel vm = issueEntity.ConvertToVoteViewModel("TestUid");

            // Assert
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("60", vm.ApprovalPercentage);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("text-danger", vm.ApprovalPercentageColor);
            Assert.AreEqual(40, vm.VoteCount);
            Assert.AreEqual("btn-success", vm.UpvoteButtonColor);
            Assert.AreEqual("btn-default", vm.DownvoteButtonColor);
        }
    }
}
