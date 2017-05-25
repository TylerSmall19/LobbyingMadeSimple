using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using LobbyingMadeSimple.Core;
using Moq;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class DetailsViewModelTest
    {
        [TestMethod]
        public void Has_gettable_and_settable_properties()
        {
            // Act
            var vm = new DetailsViewModel()
            {
                Id = 1,
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long",
                VoteCount = 12,
                IssueScope = "Federal",
                ApprovalPercentage = "60",
                FundingRaised = 2500,
                FundingGoal = 50000,
                UpvoteButtonColor = "btn-success",
                DownvoteButtonColor = "btn-default"
            };

            // Assert
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("Long", vm.LongDescription);
            Assert.AreEqual(12, vm.VoteCount);
            Assert.AreEqual("Federal", vm.IssueScope);
            Assert.AreEqual("60", vm.ApprovalPercentage);
            Assert.AreEqual(2500, vm.FundingRaised);
            Assert.AreEqual(50000, vm.FundingGoal);
            Assert.AreEqual("btn-success", vm.UpvoteButtonColor);
            Assert.AreEqual("btn-default", vm.DownvoteButtonColor);
        }

        [TestMethod]
        public void Can_be_converted_from_a_raw_entity_with_an_extension_method()
        {
            // Arrange
            Issue issueEntity = Mock.Of<Issue>(
                i => i.Title == "Title"
                && i.ShortDescription == "Short"
                && i.LongDescription == "Long"
                && i.StateAbbrev == "MO"
                && i.IsStateIssue == true
                && i.FundingGoal == 50000.00
                && i.Id == 1
                && i.FundingRaised == 2500
                && i.GetPrettyPercentage() == "60"
                && i.GetVoteForUser("test") == Mock.Of<Vote>(v => v.IsUpvote == true)
                && i.TotalVotes() == 12
            );

            // Act
            DetailsViewModel vm = issueEntity.ConvertToDetailsViewModel("test");

            // Assert
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("Long", vm.LongDescription);
            Assert.AreEqual(12, vm.VoteCount);
            Assert.AreEqual("MO", vm.IssueScope);
            Assert.AreEqual("60", vm.ApprovalPercentage);
            Assert.AreEqual(2500, vm.FundingRaised);
            Assert.AreEqual(50000, vm.FundingGoal);
            Assert.AreEqual("btn-success", vm.UpvoteButtonColor);
            Assert.AreEqual("btn-default", vm.DownvoteButtonColor);
        }
    }
}
