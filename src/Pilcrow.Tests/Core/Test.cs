
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pilcrow.Tests.Core
{
    public abstract class Test
    {
        private IEnumerable<IEnvManager> _testEnvManagers;
        
        [TestInitialize]
        public void TestInitialize()
        {
            InitializeTestMethod();
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            CleanupTestMethod();
        }
        
        protected virtual void InitializeTestMethod()
        {
            _testEnvManagers = GetTestEnvManagers();
            
            foreach (var testEnvManager in _testEnvManagers)
                testEnvManager.InitializeTestMethod(this, TestContext);
        }
        
        protected virtual void CleanupTestMethod()
        {
            foreach (var testEnvManager in _testEnvManagers)
                testEnvManager.CleanupTestMethod(this, TestContext);
            
            _testEnvManagers = null;
        }
        
        protected virtual IEnumerable<IEnvManager> GetTestEnvManagers()
        {
            return new List<IEnvManager>();
        }
        
        public TestContext TestContext { get; set; }
    }
}
