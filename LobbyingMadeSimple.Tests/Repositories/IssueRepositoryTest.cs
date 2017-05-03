using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Repositories;
using LobbyingMadeSimple.Models;
using System.Collections.Generic;
using System.Collections;

namespace LobbyingMadeSimple.Tests.Repositories
{
    [TestClass]
    public class IssueRepositoryTest
    {
        private IssueRepository _repo;
        private String DESCRIPTION;
        private String TITLE;
        private bool ISSTATEISSUE;

        [TestInitialize]
        public void TestInit()
        {
            _repo = new IssueRepository();

            InitParams();
        }

        private void InitParams()
        {
            _repo = new IssueRepository();

            DESCRIPTION = "Description";
            TITLE = "Test Data Title";
            ISSTATEISSUE = false;
        }

        [TestMethod]
        [TestCategory("CrudOperations")]
        public void IssueCrud()
        {
            var issueId = Create();
            ReadOne(issueId);
            ReadAll();
            ReadVotable();
            Update(issueId);
            Delete(issueId);
        }

        private int Create()
        {
            Issue issue = new Issue()
            {
                ShortDescription = DESCRIPTION,
                LongDescription = DESCRIPTION,
                Title = TITLE,
                IsStateIssue = ISSTATEISSUE
            };

            _repo.Add(issue);

            Assert.IsNotNull(issue.IssueID, "Issue ID is Null");

            return issue.IssueID;
        }

        private void ReadOne(int id)
        {
            Issue issue = _repo.Find(id);

            Assert.IsNotNull(issue, "Find returned null issue");
            Assert.AreEqual(id, issue.IssueID, "IDs don't match");
        }

        private void ReadVotable()
        {
            List<Issue> issues = _repo.GetAllVotableProducts();

            Assert.IsTrue(issues.Count > 0, "No results returned from Votable");
            foreach (Issue issue in issues)
            {
                Assert.IsTrue(issue.IsVotableIssue);
            }
        }

        private void ReadAll()
        {
            List<Issue> issues = _repo.GetAll();

            Assert.IsTrue(issues.Count > 0, "Repo returned no results");
            Assert.AreEqual(TITLE, issues[issues.Count - 1].Title, "Last Item doesn't match expectation");
        }

        private void Update(int id)
        {
            // Arrange
            Issue issue = _repo.Find(id);
            String newTitle = "New Testing Title";
            issue.Title = newTitle;

            // Act
            _repo.Update(issue);
            Issue newIssue = _repo.Find(id);

            // Assert
            Assert.AreEqual(newTitle, issue.Title, "Title was not updated properly");
        }

        private void Delete(int id)
        {
            Issue issue = _repo.Find(id);

            _repo.Remove(issue);
            issue = _repo.Find(id);

            Assert.IsNull(issue, "Issue was not deleted");
        }
    }
}
