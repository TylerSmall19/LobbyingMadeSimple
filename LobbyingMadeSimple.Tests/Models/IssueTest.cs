using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Models;

namespace LobbyingMadeSimple.Tests.Models
{

/// <summary>
/// Tests functionality of the Issue Model including logic to set approval and disapproval ratings
/// and determine if an Issue can be approved for funding by voting alone
/// </summary>
[TestClass]
    public class IssueTest
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ConstructorDefaults()
        {
            Issue issue = new Issue();

            Assert.AreEqual(0, issue.UpvoteCount);
            Assert.AreEqual(0, issue.DownVoteCount);
            Assert.AreEqual(1500, issue.VoteCountNeeded);
            Assert.IsFalse(issue.IsApproved());
        }

        [TestMethod]
        public void CanBeUpVoted()
        {
            Issue issue = new Issue();

            issue.AddVote(true);

            Assert.AreEqual(1, issue.UpvoteCount);
            Assert.AreEqual(0, issue.DownVoteCount);
        }

        [TestMethod]
        public void CanBeDownVoted()
        {
            Issue issue = new Issue();

            issue.AddVote(false);

            Assert.AreEqual(1, issue.DownVoteCount);
            Assert.AreEqual(0, issue.UpvoteCount);
        }

        [TestMethod]
        public void CanBeApprovedThroughVotes()
        {
            Issue issue = new Issue();

            issue.UpvoteCount = 1499;
            issue.AddVote(true);

            Assert.IsTrue(issue.IsApproved());
        }

        [TestMethod]
        public void CanBeDeniedThroughVotes()
        {
            Issue issue = new Issue();

            issue.DownVoteCount = 1499;
            issue.AddVote(false);

            Assert.IsFalse(issue.IsApproved());
        }

        [TestMethod]
        public void FalseIfLessThan67Percent()
        {
            Issue issue = new Issue();

            issue.UpvoteCount   = 999;
            issue.DownVoteCount = 500;
            issue.AddVote(false);

            Assert.IsFalse(issue.IsApproved());
        }

        [TestMethod]
        public void TrueIf67PercentOrMore()
        {
            Issue issue = new Issue();

            issue.UpvoteCount   = 999;
            issue.DownVoteCount = 500;
            issue.AddVote(true);

            Assert.IsTrue(issue.IsApproved());
        }
    }
}
