using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class DetailsViewModelTest
    {
        [TestMethod]
        public void DetailsViewModel_has_gettable_and_settable_properties()
        {
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
        }
    }
}
