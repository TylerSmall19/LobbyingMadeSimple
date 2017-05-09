using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Interfaces;
using LobbyingMadeSimple.Models;
using Moq;
using System.Collections.Generic;
using LobbyingMadeSimple.Controllers;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;

namespace LobbyingMadeSimple.Tests.Controllers
{
    [TestClass]
    public class IssuesControllerTest
    {
        private List<Issue> votableIssues;
        private List<Issue> fundableIssues;
        private Issue votableIssue;
        private Issue fundableIssue;
        private Issue newIssue;
        private IssuesController controller;
        private Mock<IIssueRepository> _repo;

        [TestInitialize]
        public void TestInit()
        {
            votableIssue = Mock.Of<Issue>(i => i.IsVotableIssue == true);
            fundableIssue = Mock.Of<Issue>(i => i.IsFundable == true);
            newIssue = Mock.Of<Issue>();

            _repo = new Mock<IIssueRepository>();
            _repo.Setup(r => r.Find(1)).Returns(votableIssue);
            _repo.Setup(r => r.Add(newIssue)).Verifiable();
                //&& r.GetAllVotableIssuesSortedByDate() == votableIssues
                //&& r.GetAllFundableIssuesSortedByDate() == fundableIssues
                //&& r.GetAllVotableIssuesSortedByVoteCount() == votableIssues

            controller = new IssuesController(_repo.Object);

            var user = Mock.Of<IPrincipal>(p => p.Identity.Name == "TestUid");
            var httpContextMock = Mock.Of<HttpContextBase>(ctx => ctx.User == user);

            var controllerContext = new ControllerContext { HttpContext = httpContextMock };
            controllerContext.Controller = controller;
            controller.ControllerContext = controllerContext;
        }

        [TestMethod]
        public void IssuesController_Details_finds_the_correct_issue_when_called()
        {
            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.AreEqual(votableIssue, result.Model);
        }

        [TestMethod]
        public void IssuesController_Details_returns_no_view_if_Issue_not_found()
        {
            // Act
            var result = controller.Details(2) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IssuesController_Details_returns_null_when_bad_request_id_is_passed()
        {
            // Act 
            var result = controller.Details(null) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IssuesController_Create_Get_returns_view()
        {
            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IssuesController_Create_Post_creates_a_new_object_and_redirects_to_index_when_passed_valid_data()
        {
            // Act
            var result = controller.Create(newIssue) as RedirectToRouteResult;

            // Assert
            _repo.Verify(r => r.Add(newIssue), Times.Exactly(1));
            Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
        }

        [TestMethod]
        public void IssuesController_Create_Post_renders_view_when_invalid_data_is_passed()
        {
            // Arrange
            controller.ModelState.AddModelError("testError", "You shall NOT pass!");

            // Act
            var result = controller.Create(newIssue) as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName); // Default View
            Assert.AreEqual(newIssue, result.Model);
        }
    }
}
