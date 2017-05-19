using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using LobbyingMadeSimple.Core;
using Moq;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class IssueViewModelTest
    {
        [TestMethod]
        public void IssueViewModel_can_be_implicitly_converted_to_an_Issue_entity()
        {
            IssueViewModel vm = new IssueViewModel()
            {
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long", 
                StateAbbrev = "MO",
                IsStateIssue = true,
                FundingGoal = 75000.00
            };

            Issue issue = vm;

            Assert.AreEqual("Title", issue.Title);
            Assert.AreEqual("Short", issue.ShortDescription);
            Assert.AreEqual("Long", issue.LongDescription);
            Assert.AreEqual("MO", issue.StateAbbrev);
            Assert.IsTrue(issue.IsStateIssue);
            Assert.AreEqual(75000.00, issue.FundingGoal);
        }

        [TestMethod]
        public void IssueViewModel_doesnt_set_StateAbbrev_if_IsStateIssue_false()
        {
            IssueViewModel vm = new IssueViewModel()
            {
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long",
                StateAbbrev = "MO",
                IsStateIssue = false,
                FundingGoal = 75000.00
            };

            Issue issue = vm;

            Assert.AreEqual("Title", issue.Title);
            Assert.AreEqual("Short", issue.ShortDescription);
            Assert.AreEqual("Long", issue.LongDescription);
            Assert.IsNull(issue.StateAbbrev);
            Assert.IsFalse(issue.IsStateIssue);
            Assert.AreEqual(75000.00, issue.FundingGoal);
        }

        [TestMethod]
        public void IssueViewModel_sets_StateAbbrev_when_IsStateIssue_true()
        {
            IssueViewModel vm = new IssueViewModel()
            {
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long",
                StateAbbrev = "MO",
                IsStateIssue = true,
                FundingGoal = 75000.00
            };

            Issue issue = vm;

            Assert.AreEqual("Title", issue.Title);
            Assert.AreEqual("Short", issue.ShortDescription);
            Assert.AreEqual("Long", issue.LongDescription);
            Assert.AreEqual("MO", issue.StateAbbrev);
            Assert.IsTrue(issue.IsStateIssue);
            Assert.AreEqual(75000.00, issue.FundingGoal);
        }

        [TestMethod]
        public void IssueViewModel_can_be_assigned_from_raw_entity()
        {
            Issue issueEntity = Mock.Of<Issue>(
                i => i.Title == "Title"
                && i.ShortDescription == "Short"
                && i.LongDescription == "Long"
                && i.StateAbbrev == "MO"
                && i.IsStateIssue == true
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
            );

            IssueViewModel vm = issueEntity;

            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("Long", vm.LongDescription);
            Assert.AreEqual("MO", vm.StateAbbrev);
            Assert.IsTrue(vm.IsStateIssue);
            Assert.AreEqual(75000.00, vm.FundingGoal);
            Assert.AreEqual("60", vm.ApprovalPercentage);
            Assert.AreEqual(1500, vm.VotesLeftUntilApproval);
            Assert.AreEqual(1, vm.Id);
        }
    }
}
