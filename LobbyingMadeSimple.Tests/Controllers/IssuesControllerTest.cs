using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Core.Interfaces;
using Moq;
using System.Collections.Generic;
using LobbyingMadeSimple.Controllers;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;
using System.Reflection;
using System.Linq;
using LobbyingMadeSimple.Core;
using LobbyingMadeSimple.Web.Models;
using DeepEqual.Syntax;

namespace LobbyingMadeSimple.Tests.Controllers
{
    [TestClass]
    public class IssuesControllerTest
    {
        private List<Issue> votableIssues;
        private List<Issue> fundableIssues;
        private Issue votableIssue;
        private Issue fundableIssue;
        private IssueViewModel newIssue;
        private IssuesController controller;
        private Mock<IIssueRepository> _repo;

        [TestInitialize]
        public void TestInit()
        {
            votableIssue = Mock.Of<Issue>(i => i.IsVotableIssue == true && i.Author.Id == "AuthId");
            fundableIssue = Mock.Of<Issue>(i => i.IsFundable == true && i.Author.Id == "AuthId");
            newIssue = Mock.Of<IssueViewModel>();
            votableIssues = new List<Issue>() { votableIssue, votableIssue, votableIssue };
            fundableIssues = new List<Issue>() { fundableIssue, fundableIssue, fundableIssue };

            _repo = new Mock<IIssueRepository>();
            _repo.Setup(r => r.Find(1)).Returns(votableIssue);
            _repo.Setup(r => r.Add(newIssue)).Verifiable();
            _repo.Setup(r => r.GetAllVotableIssuesSortedByDate()).Returns(votableIssues);
            _repo.Setup(r => r.GetAllFundableIssuesSortedByDate()).Returns(fundableIssues);
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
            _repo.Verify(r => r.Add(It.IsAny<Issue>()), Times.Once);
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

        [TestMethod]
        public void IssuesController_Delete_Get_returns_correct_view_and_model_when_called_with_valid_data()
        {
            // Act
            var result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName); // Default View
            Assert.AreEqual(votableIssue, result.Model);
        }

        [TestMethod]
        public void IssuesController_Delete_Get_returns_400_when_id_is_null()
        {
            // Act
            var result = controller.Delete(null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void IssuesController_Delete_Get_returns_404_when_issue_is_not_found()
        {
            // Act
            var result = controller.Delete(2) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void IssuesController_DeleteConfirmed_removes_and_redirects_when_issue_is_found()
        {
            // Act 
            var result = controller.DeleteConfirmed(1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            _repo.Verify(r => r.Remove(votableIssue), Times.Once);
        }

        [TestMethod]
        public void IssuesController_DeleteConfirmed_doesnt_delete_when_id_not_valid()
        {
            // Act
            var result = controller.DeleteConfirmed(2) as RedirectToRouteResult;

            // Assert
            _repo.Verify(r => r.Remove(votableIssue), Times.Never);
            _repo.Verify(r => r.Remove(null), Times.Once);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void IssuesController_Vote_sets_model_to_AllVotableIssues()
        {
            // Arrange
            var votableIssueVms = new List<IssueViewModel>();
            votableIssues.ForEach(i => votableIssueVms.Add(i));

            // Act
            var result = controller.Vote() as ViewResult;
            var resultModel = result.Model as List<IssueViewModel>;

            // Assert
            resultModel.ShouldDeepEqual(votableIssueVms);
        }
        
        [TestMethod]
        public void IssuesController_Fund_Get_returns_a_view_containing_all_fundable_issues()
        {
            // Arrange
            var fundableIssueVms = new List<IssueViewModel>();
            fundableIssues.ForEach(i => fundableIssueVms.Add(i));

            // Act
            var result = controller.Fund() as ViewResult;
            var resultModel = result.Model as List<IssueViewModel>;

            // Assert
            Assert.AreEqual("", result.ViewName); // Using default view name Fund
            resultModel.ShouldDeepEqual(fundableIssueVms);
        }
    }
}
