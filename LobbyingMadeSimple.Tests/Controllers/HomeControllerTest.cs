﻿using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Controllers;
using Moq;
using LobbyingMadeSimple.Core.Interfaces;

namespace LobbyingMadeSimple.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(new Mock<IIssueRepository>().Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(new Mock<IIssueRepository>().Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
