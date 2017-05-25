using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;

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
                FundingRaised = "150",
                FundingGoal = "50000",
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
            Assert.AreEqual("150", vm.FundingRaised);
            Assert.AreEqual("50000", vm.FundingGoal);
            Assert.AreEqual("btn-success", vm.UpvoteButtonColor);
            Assert.AreEqual("btn-default", vm.DownvoteButtonColor);
        }
    }
}
