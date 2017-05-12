using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LobbyingMadeSimple.Core;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class ContributionTest
    {
        [TestMethod]
        public void Contribution_Has_Setable_Properties()
        {
            var user = Mock.Of<ApplicationUser>();
            var date = DateTime.Now;
            var issue = Mock.Of<Issue>();

            var contrib = new Contribution()
            {
                ContributionID = 1,
                Amount = 50.00,
                Author = user,
                AuthorID = "1",
                CreatedAt = date,
                UpdatedAt = date,
                Issue = issue,
                IssueID = 1
            };

            Assert.AreEqual(1, contrib.ContributionID);
            Assert.AreEqual(50.00, contrib.Amount);
            Assert.AreEqual(user, contrib.Author);
            Assert.AreEqual("1", contrib.AuthorID);
            Assert.AreEqual(date, contrib.CreatedAt);
            Assert.AreEqual(date, contrib.UpdatedAt);
            Assert.AreEqual(issue, contrib.Issue);
            Assert.AreEqual(1, contrib.IssueID);
        }
    }
}
