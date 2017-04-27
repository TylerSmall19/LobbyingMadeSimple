using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Repositories;
using LobbyingMadeSimple.Models;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Tests.Repositories
{
    [TestClass]
    public class IssueRepositoryTest
    {
        IssueRepository _repo;
        
        [TestInitialize]
        public void TestInit()
        {
            _repo = new IssueRepository();
        }

        [TestMethod]
        [TestCategory("Connections")]
        public void CanListAllItems()
        {
            List<Issue> issueList = _repo.GetAllVotableProducts();
            Assert.IsNotNull(issueList);
        }
    }
}
