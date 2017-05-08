using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Interfaces;
using LobbyingMadeSimple.Models;
using Moq;
using System.Collections.Generic;
using LobbyingMadeSimple.Controllers;
using System.Web.Mvc;

namespace LobbyingMadeSimple.Tests.Controllers
{
    [TestClass]
    public class IssuesControllerTest
    {
        private List<Issue> votableIssues;
        private List<Issue> fundableIssues;
        private Issue votableIssue;
        private Issue fundableIssue;
        private IssuesController controller;

        [TestInitialize]
        public void TestInit()
        {
            votableIssue = Mock.Of<Issue>(i => i.IsVotableIssue == true);
            fundableIssue = Mock.Of<Issue>(i => i.IsFundable == true);
            var _repo =
                Mock.Of<IIssueRepository>(r =>
                    r.Find(1) == votableIssue
                //&& r.GetAllVotableIssuesSortedByDate() == votableIssues
                //&& r.GetAllFundableIssuesSortedByDate() == fundableIssues
                //&& r.Add(newIssue) == null
                //&& r.GetAllVotableIssuesSortedByVoteCount() == votableIssues
                );

            controller = new IssuesController(_repo);
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
    }
}
