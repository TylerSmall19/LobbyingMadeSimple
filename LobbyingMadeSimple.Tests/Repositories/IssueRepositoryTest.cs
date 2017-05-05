using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Repositories;
using LobbyingMadeSimple.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data.Entity;
using Moq;

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
        public void IssueRepo_GetAllVotableIssuesSortedByDate_returns_sorted_list_of_votable_issues()
        {
            // Arrange
            var firstIssue = Mock.Of<Issue>(i => i.CreatedAt == DateTime.Now.AddHours(2));
            var secondIssue = Mock.Of<Issue>(i => i.CreatedAt == DateTime.Now.AddHours(1));
            var thirdIssue = Mock.Of<Issue>(i => i.CreatedAt == DateTime.Now);

            List<Issue> issues = new List<Issue>()
            {
                firstIssue,
                thirdIssue,
                secondIssue
            };

            IssueRepository mockRepo = Mock.Of<IssueRepository>(r => r.GetAllVotableIssues() == issues);

            // Act
            var result = mockRepo.GetAllVotableIssuesSortedByDate();

            // Assert
            Assert.AreEqual(firstIssue, result[0]);
            Assert.AreEqual(secondIssue, result[1]);
            Assert.AreEqual(thirdIssue, result[2]);
        }

        [TestMethod]
        public void IssueRepo_GetAllVotableIssuesSortedByDate_returns_empty_if_no_issues_are_found()
        {
            // Arrange
            IssueRepository mockRepo =
                Mock.Of<IssueRepository>(r => r.GetAllVotableIssues() == new List<Issue>());

            // Act
            var result = mockRepo.GetAllVotableIssuesSortedByDate();

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void IssueRepo_GetAllVotableIssuesSortedByVoteCount_returns_a_correctly_sorted_list()
        {
            // Arrange
            var firstIssue = Mock.Of<Issue>(i => i.Votes.Count == 750);
            var secondIssue = Mock.Of<Issue>(i => i.Votes.Count == 500);
            var thirdIssue = Mock.Of<Issue>(i => i.Votes.Count == 250);

            List<Issue> issues = new List<Issue>()
            {
                firstIssue,
                thirdIssue,
                secondIssue
            };

            IssueRepository mockRepo = Mock.Of<IssueRepository>(r => r.GetAllVotableIssues() == issues);

            // Act
            var result = mockRepo.GetAllVotableIssuesSortedByVoteCount();

            // Assert
            Assert.AreEqual(firstIssue, result[0]);
            Assert.AreEqual(secondIssue, result[1]);
            Assert.AreEqual(thirdIssue, result[2]);
        }

        [TestMethod]
        public void IssueRepo_GetAllVotableIssuesSortedByVoteCount_returns_empty_if_no_issues_are_found()
        {
            // Arrange
            IssueRepository mockRepo = 
                Mock.Of<IssueRepository>(r => r.GetAllVotableIssues() == new List<Issue>());

            // Act
            var result = mockRepo.GetAllVotableIssuesSortedByVoteCount();

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        [TestCategory("CrudOperations")]
        public void IssueCrud()
        {
            // Create
            var issueId = Create();
            // Read
            ReadOne(issueId);
            ReadAll();
            ReadVotable();
            // Update
            Update(issueId);
            // Delete
            Delete(issueId);
        }

        private int Create()
        {
            // Arrange
            Issue issue = new Issue()
            {
                ShortDescription = DESCRIPTION,
                LongDescription = DESCRIPTION,
                Title = TITLE,
                IsStateIssue = ISSTATEISSUE
            };

            // Act
            _repo.Add(issue);

            // Assert
            Assert.IsNotNull(issue.IssueID, "Issue ID is Null");

            return issue.IssueID;
        }

        private void ReadOne(int id)
        {
            // Act
            Issue issue = _repo.Find(id);

            // Assert
            Assert.IsNotNull(issue, "Find returned null issue");
            Assert.AreEqual(id, issue.IssueID, "IDs don't match");
        }

        private void ReadVotable()
        {
            // Act
            List<Issue> issues = _repo.GetAllVotableIssues();

            // Assert
            Assert.IsTrue(issues.Count > 0, "No results returned from Votable");
            foreach (Issue issue in issues)
            {
                Assert.IsTrue(issue.IsVotableIssue);
            }
        }

        private void ReadAll()
        {
            // Act
            List<Issue> issues = _repo.GetAll();

            // Assert
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
            // Arrange
            Issue issue = _repo.Find(id);

            // Act
            _repo.Remove(issue);
            issue = _repo.Find(id);

            // Assert
            Assert.IsNull(issue, "Issue was not deleted");
        }
    }
}
