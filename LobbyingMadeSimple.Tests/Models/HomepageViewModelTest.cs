using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using Moq;
using System.Collections.Generic;
using DeepEqual.Syntax;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class HomepageViewModelTest
    {
        [TestMethod]
        public void Has_settable_and_gettable_properties()
        {
            // Arrange
            var votableVm = Mock.Of<VotableHomeIssueViewModel>();
            var votableVms = new List<VotableHomeIssueViewModel>() { votableVm, votableVm };
            var fundableVm = Mock.Of<FundableHomeIssueViewModel>();
            var fundableVms = new List<FundableHomeIssueViewModel>() { fundableVm, fundableVm };

            // Act
            var vm = new HomePageViewModel()
            {
                VotableIssues = votableVms,
                FundableIssues = fundableVms
            };

            // Assert
            vm.VotableIssues.ShouldDeepEqual(votableVms);
            vm.FundableIssues.ShouldDeepEqual(fundableVms);
        }
    }
}
