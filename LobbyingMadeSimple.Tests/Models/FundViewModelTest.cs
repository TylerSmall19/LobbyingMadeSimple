using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using LobbyingMadeSimple.Core;
using Moq;
using System.Collections.Generic;
using LobbyingMadeSimple.Models;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class FundViewModelTest
    {
        [TestMethod]
        public void FundViewModel_can_be_converted_from_an_issue_entity_implicitly()
        {
            // Arrange
            Issue issue = new Issue()
            {
                Id = 1,
                AuthorID = "AuthId",
                Author = Mock.Of<ApplicationUser>(),
                CreatedAt = DateTime.Now,
                FundingGoal = 50000.00,
                FundingRaised = 0.00,
                IsFundable = true,
                IsStateIssue = true,
                IsVotableIssue = true,
                LongDescription = "Long",
                ShortDescription = "Short",
                StateAbbrev = "MO",
                Title = "Title",
                UpdatedAt = null,
                VoteCountNeeded = 15000,
                Votes = new List<Vote>()
            };

            // Act
            FundViewModel fundVm = issue;

            // Assert
            Assert.AreEqual(issue.Id, fundVm.Id);
            Assert.AreEqual(issue.Title, fundVm.Title);
            Assert.AreEqual(issue.ShortDescription, fundVm.ShortDescription);
            Assert.AreEqual(issue.FundingGoal, fundVm.FundingGoal);
            Assert.AreEqual(issue.FundingRaised, fundVm.FundingRaised);
        }

        [TestMethod]
        public void FundViewModel_GetPrettyFundingPercentage_returns_valid_percentage_when_nothing_funded()
        {
            // Arrange
            FundViewModel vm = new FundViewModel()
            {
                FundingRaised = 0,
                FundingGoal  = 50000
            };

            // Act
            string result = vm.GetPrettyFundingPercentage();

            // Assert
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void FundViewModel_GetPrettyFundingPercentage_returns_valid_percent_when_half_raised()
        {
            // Arrange
            FundViewModel vm = new FundViewModel()
            {
                FundingRaised = 25000,
                FundingGoal = 50000
            };

            // Act
            string result = vm.GetPrettyFundingPercentage();

            // Assert
            Assert.AreEqual("50", result);
        }

        [TestMethod]
        public void FundViewModel_GetPrettyFundingPercentage_returns_correct_value_when_67_raised()
        {
            // Arrange
            FundViewModel vm = new FundViewModel()
            {
                FundingRaised = 200000,
                FundingGoal = 300000
            };

            // Act
            string result = vm.GetPrettyFundingPercentage();

            // Assert
            Assert.AreEqual("67", result);
        }

        [TestMethod]
        public void FundViewModel_GetPrettyFundingPercentage_returns_correct_value_when_210_raised()
        {
            // Arrange
            FundViewModel vm = new FundViewModel()
            {
                FundingRaised = 210,
                FundingGoal = 100
            };

            // Act
            string result = vm.GetPrettyFundingPercentage();

            // Assert
            Assert.AreEqual("210", result);
        }
    }
}
