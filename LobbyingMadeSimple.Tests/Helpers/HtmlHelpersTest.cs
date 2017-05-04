using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Helpers;

namespace LobbyingMadeSimple.Tests.Helpers
{
    [TestClass]
    public class HtmlHelpersTest
    {
        [TestMethod]
        public void HtmlHelper_GetCssClassForVotePercentage_returns_correct_value_when_passing_percent()
        {
            // Act
            string result = HtmlHelpers.GetCssClassForVotePercentage("67");

            // Assert
            Assert.AreEqual("text-success", result);
        }

        [TestMethod]
        public void HtmlHelper_GetCssClassForVotePercentage_returns_correct_value_when_failing_percent()
        {
            // Act
            string result = HtmlHelpers.GetCssClassForVotePercentage("66");

            // Assert
            Assert.AreEqual("text-danger", result);
        }
    }
}
