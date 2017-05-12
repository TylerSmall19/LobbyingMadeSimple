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
            votableIssues = new List<Issue>() { votableIssue, votableIssue, votableIssue };

            _repo = new Mock<IIssueRepository>();
            _repo.Setup(r => r.Find(1)).Returns(votableIssue);
            _repo.Setup(r => r.Add(newIssue)).Verifiable();
            _repo.Setup(r => r.GetAllVotableIssuesSortedByDate()).Returns(votableIssues);
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

        //[TestMethod]
        //public void IssuesController_Create_Post_filters_allows_accepted_params_only()
        //{
        //    // Arrange
        //    var issue = new Issue()
        //    {
        //        LongDescription = "L",
        //        ShortDescription = "S",
        //        Title = "T",
        //        FundingGoal = 10,
        //        IsStateIssue = true,
        //        StateAbbrev = "MO",
        //        IsVotableIssue = true
        //    };
        //    _repo.Setup(r => r.Add(issue)).Callback((Issue i) => Assert.AreEqual(null, i.IsVotableIssue));
        //    IssuesController iController = new IssuesController(_repo.Object);

        //    // Act
        //    iController.PreBindModel(issue, "Create");
        //    var result = iController.Create(issue) as RedirectToRouteResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

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
        public void IssuesController_Vote_sets_model_to_AllVotable_and_renders_a_view()
        {
            // Act
            var result = controller.Vote() as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(votableIssues, result.Model);
            _repo.Verify(r => r.GetAllVotableIssuesSortedByDate(), Times.Once);
        }
    }

    #region Test Helper Class
    public static class TestHelpers {
        public static void PreBindModel(this Controller controller, Issue model, string operationName)
        {
            // Find the params of the action that accepts a model (create/edit in this case can take models)
            foreach (var paramToAction in controller.GetType().GetMethod(operationName,
                BindingFlags.Public,
                null,
                CallingConventions.Any,
                new Type[] { typeof(Issue) },
                null)
                .GetParameters())
            {
                // Get the attributes that can be written based on each param
                foreach (BindAttribute bindAttribute in paramToAction.GetCustomAttributes(true))
                {
                    var propertiesToReset = typeof(Issue).GetProperties().Where(x => bindAttribute.IsPropertyAllowed(x.Name) == false);

                    // Reset each Property if it's not included in the list of bound properties
                    foreach (var propertyToReset in propertiesToReset)
                    {
                        propertyToReset.SetValue(model, null);
                    }
                }
            }
        }
    }
    #endregion
}
