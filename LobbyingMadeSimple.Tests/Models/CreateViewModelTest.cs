using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using LobbyingMadeSimple.Core;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class CreateViewModelTest
    {
        [TestMethod]
        public void Has_settable_and_gettable_properties()
        {
            // Act
            var vm = new CreateViewModel()
            {
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long",
                FundingGoal = 15000,
                IsStateIssue = true,
                StateAbbrev = "MO"
            };

            // Assert
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("Long", vm.LongDescription);
            Assert.AreEqual(15000, vm.FundingGoal);
            Assert.IsTrue(vm.IsStateIssue);
            Assert.AreEqual("MO", vm.StateAbbrev);
        }

        [TestMethod]
        public void Can_be_implicitly_converted_to_an_Issue_entity()
        {
            var vm = new CreateViewModel()
            {
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long",
                StateAbbrev = "MO",
                IsStateIssue = true,
                FundingGoal = 75000
            };

            Issue issue = vm;

            Assert.AreEqual("Title", issue.Title);
            Assert.AreEqual("Short", issue.ShortDescription);
            Assert.AreEqual("Long", issue.LongDescription);
            Assert.AreEqual("MO", issue.StateAbbrev);
            Assert.IsTrue(issue.IsStateIssue);
            Assert.AreEqual(75000, issue.FundingGoal);
        }

        [TestMethod]
        public void Does_not_set_Issue_state_abbrev_when_is_not_StateIssue()
        {
            // Arrange
            var vm = new CreateViewModel()
            {
                StateAbbrev = "MO",
                IsStateIssue = false
            };

            // Act
            Issue issue = vm;

            // Assert
            Assert.IsNull(issue.StateAbbrev);
        }

        [TestMethod]
        public void Sets_Issue_state_abbrev_when_is_StateIssue()
        {
            // Arrange
            var vm = new CreateViewModel()
            {
                StateAbbrev = "MO",
                IsStateIssue = true
            };

            // Act
            Issue issue = vm;

            // Assert
            Assert.AreEqual("MO", issue.StateAbbrev);
        }

        [TestMethod]
        public void Contructor_defaults_to_correct_funding_values()
        {
            // Act
            var vm = new CreateViewModel();

            // Assert
            Assert.AreEqual(50000, vm.FundingGoal);
        }
    }
}
