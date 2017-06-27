using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Db;
using Pilcrow.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Pilcrow.Tests.Core
{
    public abstract class IntegrationTest
    {
        protected TestServer Server;
        
        protected HttpClient Client;
        
        protected IContext DbContext;
        
        public TestContext TestContext { get; set; }
        
        public string TestContentRoot => Directory
            .GetParent(Directory.GetCurrentDirectory())
            .Parent
            .Parent
            .FullName;
        
        public string TestDatabaseName => string.Join("_", new List<string>
        {
            GetType().Name.Replace('.', '_'),
            TestContext.TestName
        }).ToLower();
        
        [TestInitialize]
        public void TestInitialize()
        {
            Environment.SetEnvironmentVariable(
                "Database:DatabaseName",
                TestDatabaseName
            );
            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(TestContentRoot)
                .UseStartup<Startup>();
            Server = new TestServer(webHostBuilder);
            Client = Server.CreateClient();
            DbContext = Server.Host.Services.GetService<IContext>();
            InitializeTestMethod();
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            CleanupTestMethod();
            DbContext.DropDatabase(TestDatabaseName);
            DbContext = null;
            Client = null;
            Server = null;
        }
        
        protected virtual void InitializeTestMethod()
        {
        }
        
        protected virtual void CleanupTestMethod()
        {
        }
    }
}
