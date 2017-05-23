using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Helpers;
using Moq;
using System.Collections.Generic;
using LobbyingMadeSimple.Core;

namespace LobbyingMadeSimple.Tests.Helpers
{
    [TestClass]
    public class HtmlHelpersTest
    {
        [TestMethod]
        public void GetCssClassForVotePercentage_returns_correct_value_when_passing_percent()
        {
            // Act
            string result = HtmlHelpers.GetCssClassForVotePercentage("67");

            // Assert
            Assert.AreEqual("text-success", result);
        }

        [TestMethod]
        public void GetCssClassForVotePercentage_returns_correct_value_when_failing_percent()
        {
            // Act
            string result = HtmlHelpers.GetCssClassForVotePercentage("66");

            // Assert
            Assert.AreEqual("text-danger", result);
        }

        [TestMethod]
        public void GetVoteButtonColor_returns_correct_color_when_vote_exists_and_is_upvote()
        {
            // Arrange
            string uid = "test";
            Issue issue = Mock.Of<Issue>(i => i.GetVoteForUser(uid) == Mock.Of<Vote>(v => v.IsUpvote == true));

            // Act
            string resultUpvote = HtmlHelpers.GetVoteButtonColor(uid, issue, true);
            string resultDownvote = HtmlHelpers.GetVoteButtonColor(uid, issue, false);

            // Assert
            Assert.AreEqual("btn-success", resultUpvote);
            Assert.AreEqual("btn-default", resultDownvote);
        }

        [TestMethod]
        public void GetVoteButtonColor_returns_correct_color_when_vote_exists_and_is_downvote()
        {
            // Arrange
            string uid = "test";
            Issue issue = Mock.Of<Issue>(i => i.GetVoteForUser(uid) == Mock.Of<Vote>(v => v.IsUpvote == false));

            // Act
            string resultUpvote = HtmlHelpers.GetVoteButtonColor(uid, issue, true);
            string resultDownvote = HtmlHelpers.GetVoteButtonColor(uid, issue, false);

            // Assert
            Assert.AreEqual("btn-default", resultUpvote);
            Assert.AreEqual("btn-danger", resultDownvote);
        }

        [TestMethod]
        public void GetVoteButtonColor_returns_correct_color_when_vote_doesnt_exist()
        {
            // Arrange
            string uid = "test";
            Issue issue = Mock.Of<Issue>();

            // Act
            string resultUpvote = HtmlHelpers.GetVoteButtonColor(uid, issue, true);
            string resultDownvote = HtmlHelpers.GetVoteButtonColor(uid, issue, false);

            // Assert
            Assert.AreEqual("btn-primary", resultUpvote);
            Assert.AreEqual("btn-primary", resultDownvote);
        }
    }
}
