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
        public void IssueViewModel_can_be_assigned_from_raw_entity()
        {
            Issue issueEntity = new Issue()
            {
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long",
                StateAbbrev = "MO",
                IsStateIssue = true,
                FundingGoal = 75000.00,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Id = 1,
                AuthorID = "authorId",
                FundingRaised = 2500,
                IsFundable = true,
                IsVotableIssue = false,
                VoteCountNeeded = 0,
                Votes = new List<Vote>()
            };

            IssueViewModel vm = issueEntity;

            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("Long", vm.LongDescription);
            Assert.AreEqual("MO", vm.StateAbbrev);
            Assert.IsTrue(vm.IsStateIssue);
            Assert.AreEqual(75000.00, vm.FundingGoal);
        }
    }
}
