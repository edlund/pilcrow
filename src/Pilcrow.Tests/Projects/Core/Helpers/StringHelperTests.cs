using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Core.Helpers;
using Pilcrow.Tests.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pilcrow.Tests.Projects.Core.Helpers
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void SlugifyTest()
        {
            Assert.AreEqual("hello-world", StringHelper.Slugify("Hello World!"));
            Assert.AreEqual(
                "ostlund-ahmans-akta-uberjaktfar",
                StringHelper.Slugify("Östlund Åhman's Äkta ÜberJaktFår")
            );
        }
        
        [TestMethod]
        public void CamelifyTest()
        {
            Assert.AreEqual("HelloWorld", StringHelper.Camelify("Hello World!"));
            Assert.AreEqual(
                "OstlundAhmansAktaUberjaktfar",
                StringHelper.Camelify("Östlund Åhman's Äkta ÜberJaktFår")
            );
        }
        
        [TestMethod]
        public void ToTitleCaseTest()
        {
            Assert.AreEqual("A B C", StringHelper.ToTitleCase("a b c"));
            Assert.AreEqual(
                "Quoth The Raven \"nevermore\".",
                StringHelper.ToTitleCase("quoth the raven \"nevermore\".")
            );
        }
    }
}
