using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using Moq;
using LobbyingMadeSimple.Core;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class FundableHomeIssueViewModelTest
    {
        [TestMethod]
        public void Has_gettable_and_settable_properties()
        {
            // Act
            var vm = new FundableHomeIssueViewModel()
            {
                Id = 1,
                Title = "Title",
                ShortDescription = "Short"
            };

            // Assert
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("Short", vm.ShortDescription);
        }

        [TestMethod]
        public void Can_be_created_implicitly_from_an_entity()
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
            FundableHomeIssueViewModel vm = issueEntity;

            // Assert
            Assert.AreEqual(issueEntity.Title, vm.Title);
            Assert.AreEqual(issueEntity.Id, vm.Id);
            Assert.AreEqual(issueEntity.ShortDescription, vm.ShortDescription);
        }
    }
}
