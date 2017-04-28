using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LobbyingMadeSimple.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LobbyingMadeSimple.Tests.Helpers
{
    [TestClass]
    public class StateListTest
    {
        [TestMethod]
        public void Returns52StateOptions()
        {
            List<SelectListItem> list = (List<SelectListItem>) StateListHelpers.GetAllStates();

            Assert.AreEqual(52, list.Count, "Number of States isn't 50 + DC + Select State option");
        }
    }
}
