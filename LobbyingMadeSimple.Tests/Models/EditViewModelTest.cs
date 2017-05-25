using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Web.Models;
using LobbyingMadeSimple.Core;
using Moq;

namespace LobbyingMadeSimple.Tests.Models
{
    [TestClass]
    public class EditViewModelTest
    {
        [TestMethod]
        public void Has_gettable_and_settable_properties()
        {
            // Act
            var vm = new EditViewModel()
            {
                Id = 1,
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long"
            };

            // Assert
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("Long", vm.LongDescription);
        }

        [TestMethod]
        public void Can_be_converted_from_entity()
        {
            // Arrange
            var issue = Mock.Of<Issue>(i => i.Title == "Title" && i.Id == 2 && i.ShortDescription == "Short" && i.LongDescription == "Long");

            // Act
            EditViewModel vm = issue;

            // Assert
            Assert.AreEqual(2, vm.Id);
            Assert.AreEqual("Title", vm.Title);
            Assert.AreEqual("Short", vm.ShortDescription);
            Assert.AreEqual("Long", vm.LongDescription);
        }

        [TestMethod]
        public void Contains_an_extension_to_map_vm_to_issue_for_editing()
        {
            // Arrange
            var issue = Mock.Of<Issue>();
            var vm = new EditViewModel()
            {
                Id = 2,
                Title = "Title",
                ShortDescription = "Short",
                LongDescription = "Long"
            };

            // Act
            issue.MapFromEditVm(vm);

            // Assert
            Assert.AreEqual("Title", issue.Title);
            Assert.AreEqual("Short", issue.ShortDescription);
            Assert.AreEqual("Long", issue.LongDescription);
            Assert.AreEqual(0, issue.Id);
        }
    }
}
