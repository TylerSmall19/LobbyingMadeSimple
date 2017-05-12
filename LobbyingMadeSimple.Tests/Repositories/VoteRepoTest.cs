using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Repositories;
using LobbyingMadeSimple.Models;
using System.Collections.Generic;
using Moq;
using LobbyingMadeSimple.Core.Interfaces;
using System.Data.Entity;
using System.Linq;
using Core;
using LobbyingMadeSimple.DAL;

namespace LobbyingMadeSimple.Tests.Repositories
{
    [TestClass]
    public class VoteRepoTest
    {
        private VoteRepository _repo;
        private String authorID;
        private Issue issue;
        private Vote vote;
        private bool isUpVote;
        private Mock<IIssueRepository> mockIssueRepo;
        private Mock<ApplicationDbContext> mockContext;
        private Mock<Issue> mockIssue;

        [TestInitialize]
        public void TestInit()
        {
            InitParams();

            _repo = new VoteRepository(mockIssueRepo.Object, mockContext.Object);
        }

        private void InitParams()
        {
            authorID = new Guid().ToString();
            isUpVote = true;
            
            // Create a Vote
            vote = new Vote()
            {
                AuthorID = authorID,
                IssueID = 1,
                IsUpvote = isUpVote
            };

            // Set up Vote data
            var data = new List<Vote>()
            {
                // Non-filtered Votes
                vote, vote,

                // Filtered Vote
                new Vote()
                {
                    AuthorID = authorID,
                    IssueID = 2,
                    IsUpvote = true
                }
            }.AsQueryable();

            // Mocks the Issue
            mockIssue = new Mock<Issue>();

            issue = mockIssue.Object;

            // Mock the Votes DbSet to avoid changes to the real DB
            var mockVotes = new Mock<DbSet<Vote>>();
            mockVotes.As<IQueryable<Vote>>().Setup(m => m.Provider).Returns(data.Provider);
            mockVotes.As<IQueryable<Vote>>().Setup(m => m.Expression).Returns(data.Expression);
            mockVotes.As<IQueryable<Vote>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockVotes.As<IQueryable<Vote>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Mock the DbContext
            mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Votes)
                .Returns(mockVotes.Object);

            // Mock the Issue repo
            mockIssueRepo = new Mock<IIssueRepository>();
            mockIssueRepo.Setup(m => m.Find(1))
                .Returns(issue);
        }

        [TestMethod]
        public void ReadAllVotesForIssue_filters_results_without_proper_id()
        { 
            // Act
            List<Vote> votes = _repo.GetAllVotesForIssue(1);

            // Assert
            Assert.IsTrue(votes.Count == 2, "Returned result count didn't match 2");
            Assert.AreEqual(vote, votes[0]);
            Assert.AreEqual(vote, votes[1]);
        }
    }
}
