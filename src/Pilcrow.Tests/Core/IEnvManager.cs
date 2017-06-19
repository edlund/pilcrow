
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pilcrow.Tests.Core
{
    public interface IEnvManager
    {
        void InitializeTestMethod(Test test, TestContext testContext);
        
        void CleanupTestMethod(Test test, TestContext testContext);
    }
}
