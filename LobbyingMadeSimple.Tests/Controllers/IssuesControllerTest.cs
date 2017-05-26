using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Core.Interfaces;
using Moq;
using System.Collections.Generic;
using LobbyingMadeSimple.Controllers;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;
using LobbyingMadeSimple.Core;
using LobbyingMadeSimple.Web.Models;
using DeepEqual.Syntax;
using PagedList;

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
            votableIssue = Mock.Of<Issue>(i => i.IsVotableIssue == true && i.Author.Id == "AuthId" && i.GetPrettyPercentage() == "67");
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
        public void Details_finds_the_correct_issue_when_called()
        {
            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            result.Model.ShouldDeepEqual(votableIssue.ConvertToDetailsViewModel(""));
        }

        [TestMethod]
        public void Details_returns_no_view_if_Issue_not_found()
        {
            // Act
            var result = controller.Details(2) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Details_returns_null_when_bad_request_id_is_passed()
        {
            // Act 
            var result = controller.Details(null) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Edit_Get_returns_an_EditViewModel_for_the_correct_issue()
        {
            // Arrange
            var editableIssue = Mock.Of<Issue>(i => i.Title == "Title" && i.Id == 1 && i.ShortDescription == "Short" && i.LongDescription == "Long");
            _repo = new Mock<IIssueRepository>();
            _repo.Setup(r => r.Find(1)).Returns(editableIssue);
            var newController = new IssuesController(_repo.Object);

            // Act
            var result = newController.Edit(1) as ViewResult;

            // Assert
            result.Model.ShouldDeepEqual((EditViewModel)editableIssue);
        }

        [TestMethod]
        public void Edit_Get_returns_a_404_when_no_issue_is_found()
        {
            // Act
            var result = controller.Edit(4) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void Edit_Get_returns_404_when_negative_id_is_passed()
        {
            // Act
            var result = controller.Edit(-1) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void Edit_Post_returns_a_redirect_when_values_are_valid()
        {
            // Arrange
            var vm = Mock.Of<EditViewModel>(v => v.Id == 1 && v.Title == "Title" && v.ShortDescription == "Short" && v.LongDescription == "Long");

            // Act
            var result = controller.Edit(vm) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Edit_Post_returns_a_view_when_ModelState_is_invalid()
        {
            // Arrange
            controller.ModelState.AddModelError("", "");
            var vm = Mock.Of<EditViewModel>(v => v.Id == 1 && v.Title == "Title" && v.ShortDescription == "Short" && v.LongDescription == "Long");

            // Act
            var result = controller.Edit(vm) as ViewResult;

            // Assert
            result.Model.ShouldDeepEqual(vm);
        }

        [TestMethod]
        public void Create_Get_returns_view()
        {
            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_Post_creates_a_new_object_and_redirects_to_index_when_passed_valid_data()
        {
            // Act
            var result = controller.Create(new CreateViewModel()) as RedirectToRouteResult;

            // Assert
            _repo.Verify(r => r.Add(It.IsAny<Issue>()), Times.Once);
            Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
        }

        [TestMethod]
        public void Create_Post_renders_view_when_invalid_data_is_passed()
        {
            // Arrange
            controller.ModelState.AddModelError("testError", "You shall NOT pass!");
            var createVm = new CreateViewModel();

            // Act
            var result = controller.Create(createVm) as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName); // Default View
            result.Model.ShouldDeepEqual(createVm);
        }

        [TestMethod]
        public void Delete_Get_returns_correct_view_and_model_when_called_with_valid_data()
        {
            // Act
            var result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName); // Default View
            result.Model.ShouldDeepEqual(votableIssue.ConvertToDetailsViewModel(""));
        }

        [TestMethod]
        public void Delete_Get_returns_400_when_id_is_null()
        {
            // Act
            var result = controller.Delete(null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void Delete_Get_returns_404_when_issue_is_not_found()
        {
            // Act
            var result = controller.Delete(2) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void DeleteConfirmed_removes_and_redirects_when_issue_is_found()
        {
            // Act 
            var result = controller.DeleteConfirmed(1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            _repo.Verify(r => r.Remove(votableIssue), Times.Once);
        }

        [TestMethod]
        public void DeleteConfirmed_doesnt_delete_when_id_not_valid()
        {
            // Act
            var result = controller.DeleteConfirmed(2) as RedirectToRouteResult;

            // Assert
            _repo.Verify(r => r.Remove(votableIssue), Times.Never);
            _repo.Verify(r => r.Remove(null), Times.Once);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Vote_sets_model_to_AllVotableIssues()
        {
            // Arrange
            var votableIssueVms = new List<VoteViewModel>();
            votableIssues.ForEach(i => votableIssueVms.Add(i.ConvertToVoteViewModel("test")));

            // Act
            var result = controller.Vote(1) as ViewResult;
            var resultModel = result.Model;

            // Assert
            // This test will fail if page numbers are changed dramatically
            resultModel.ShouldDeepEqual(votableIssueVms.ToPagedList(1, 12));
        }
        
        [TestMethod]
        public void Fund_Get_returns_a_view_containing_all_fundable_issues()
        {
            // Arrange
            var fundableIssueVms = new List<FundViewModel>();
            fundableIssues.ForEach(i => fundableIssueVms.Add(i));
            var pageNum = 1;

            // Act
            var result = controller.Fund(pageNum) as ViewResult;
            var resultModel = result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName); // Using default view name Fund
            resultModel.ShouldDeepEqual(fundableIssueVms.ToPagedList(pageNum, 15));
        }
    }
}
