using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Core.Helpers;
using Pilcrow.Tests.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pilcrow.Tests.Projects.Core.Helpers
{
    [TestClass]
    public class AssemblyHelperTests
    {
        [TestMethod]
        public void GetRuntimeAssembliesTest()
        {
            var projectAssemblies = AssemblyHelper.GetRuntimeAssemblies(
                assemblyName => assemblyName.Name.StartsWith("Pilcrow.")
            );
            Assert.IsTrue(projectAssemblies.Count() > 0);
        }
    }
}
