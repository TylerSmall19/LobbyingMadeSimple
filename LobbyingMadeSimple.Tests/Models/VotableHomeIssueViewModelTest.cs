using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using LobbyingMadeSimple.Core;
using Moq;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class VotableHomeIssueViewModelTest
    {
        [TestMethod]
        public void Has_readable_and_setable_properties()
        {
            // Act
            var vm = new VotableHomeIssueViewModel()
            {
                Id = 1,
                Title = "Title",
                ApprovalPercentage = "60",
                ShortDescription = "Short",
                ApprovalPercentageColor = "text-success",
                IssueScope = "Federal",
                VoteCount = 40
            };

            // Assert
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("60", vm.ApprovalPercentage);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("text-success", vm.ApprovalPercentageColor);
            Assert.AreEqual("Federal", vm.IssueScope);
            Assert.AreEqual(40, vm.VoteCount);
        }

        [TestMethod]
        public void Can_be_created_implicitly_from_an_issue_entity()
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
            );

            // Act
            VotableHomeIssueViewModel vm = issueEntity;

            // Assert
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("60", vm.ApprovalPercentage);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual(40, vm.VoteCount);
            Assert.AreEqual("text-danger", vm.ApprovalPercentageColor);
        }

        [TestMethod]
        public void Operator_sets_correct_display_string_when_federal()
        {
            // Arrange
            var issue = Mock.Of<Issue>(i => i.IsStateIssue == false && i.GetPrettyPercentage() == "60");

            // Act
            VotableHomeIssueViewModel vm = issue;

            // Assert
            Assert.AreEqual("Federal", vm.IssueScope);
        }

        [TestMethod]
        public void Operator_sets_correct_display_string_when_not_fedeal_issue()
        {
            // Arrange
            var issue = Mock.Of<Issue>(i => i.IsStateIssue == true && i.StateAbbrev == "MO" && i.GetPrettyPercentage() == "60");

            // Act
            VotableHomeIssueViewModel vm = issue;

            // Assert
            Assert.AreEqual("State: MO", vm.IssueScope);
        }
    }
}
