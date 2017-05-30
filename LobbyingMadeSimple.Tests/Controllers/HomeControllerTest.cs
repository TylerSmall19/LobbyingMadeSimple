using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Controllers;
using Moq;
using LobbyingMadeSimple.Core.Interfaces;
using System.Collections.Generic;
using LobbyingMadeSimple.Core;
using LobbyingMadeSimple.Web.Models;
using DeepEqual.Syntax;

namespace LobbyingMadeSimple.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_returns_a_view()
        {
            // Arrange
            var repo = new Mock<IIssueRepository>();
            repo.Setup(r => r.GetTopVotableIssues(15)).Returns(new List<Issue>());
            repo.Setup(r => r.GetAllFundableIssuesSortedByDate()).Returns(new List<Issue>());
            HomeController controller = new HomeController(repo.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName); // Action uses the default view
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index_returns_a_homepage_view_model()
        {
            // Arrange
            var repo = new Mock<IIssueRepository>();
            repo.Setup(r => r.GetTopVotableIssues(15)).Returns(new List<Issue>());
            repo.Setup(r => r.GetAllFundableIssuesSortedByDate()).Returns(new List<Issue>());
            HomeController controller = new HomeController(repo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var model = result.Model;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(HomePageViewModel));
        }
        
        [TestMethod]
        public void Index_returns_valid_homepage_view_model_when_results_are_found()
        {
            // Arrange
            var votableIssue = Mock.Of<Issue>(i => i.IsVotableIssue == true && i.GetPrettyPercentage() == "67" && i.Id == 1);
            var fundableIssue = Mock.Of<Issue>(i => i.IsFundable == true && i.Id == 2);
            var votableList = new List<Issue>() { votableIssue, votableIssue };
            var fundableList = new List<Issue>() { fundableIssue, fundableIssue };

            var repo = new Mock<IIssueRepository>();
            repo.Setup(r => r.GetTopVotableIssues(15)).Returns(votableList);
            repo.Setup(r => r.GetAllFundableIssuesSortedByDate()).Returns(fundableList);
            HomeController controller = new HomeController(repo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            HomePageViewModel model = (HomePageViewModel) result.Model;

            // Assert
            foreach(VotableHomeIssueViewModel vm in model.VotableIssues)
            {
                Assert.AreEqual(votableIssue.GetPrettyPercentage(), vm.ApprovalPercentage);
                Assert.AreEqual("text-success", vm.ApprovalPercentageColor);
                Assert.AreEqual(votableIssue.Id, vm.Id);
            }

            foreach(FundableHomeIssueViewModel vm in model.FundableIssues)
            {
                Assert.AreEqual(fundableIssue.Id, vm.Id);
            }
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(new Mock<IIssueRepository>().Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
