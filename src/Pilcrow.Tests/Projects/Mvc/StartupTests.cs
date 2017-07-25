using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Mvc;
using Pilcrow.Tests.Core;

namespace Pilcrow.Tests.Projects.Mvc
{
    public class DummyStartup : IStartable
    {
        public static bool ConfigureCalled { get; set; }
        
        public static bool ConfigureRoutesCalled { get; set; }
        
        public static bool ConfigureServicesCalled { get; set; }
        
        public void Configure(
            IConfigurationRoot configuration,
            IApplicationBuilder applicationBuilder,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory)
        {
            Assert.IsNotNull(configuration);
            Assert.IsNotNull(applicationBuilder);
            Assert.IsNotNull(hostingEnvironment);
            Assert.IsNotNull(loggerFactory);
            ConfigureCalled = true;
        }
        
        public void ConfigureRoutes(
            IConfigurationRoot configuration,
            IApplicationBuilder applicationBuilder,
            IHostingEnvironment hostingEnvironment,
            IRouteBuilder routeBuilder)
        {
            Assert.IsNotNull(configuration);
            Assert.IsNotNull(applicationBuilder);
            Assert.IsNotNull(hostingEnvironment);
            Assert.IsNotNull(routeBuilder);
            ConfigureRoutesCalled = true;
        }
        
        public void ConfigureServices(
            IConfigurationRoot configuration,
            IServiceCollection services)
        {
            Assert.IsNotNull(configuration);
            Assert.IsNotNull(services);
            ConfigureServicesCalled = true;
        }
    }
    
    [TestClass]
    public class StartupTests : IntegrationTest
    {
        [TestInitialize]
        public new void TestInitialize()
        {
            DummyStartup.ConfigureCalled = false;
            DummyStartup.ConfigureRoutesCalled = false;
            DummyStartup.ConfigureServicesCalled = false;
            base.TestInitialize();
        }
        
        [TestMethod]
        public void CallbackTest()
        {
            Assert.IsTrue(DummyStartup.ConfigureCalled);
            Assert.IsTrue(DummyStartup.ConfigureRoutesCalled);
            Assert.IsTrue(DummyStartup.ConfigureServicesCalled);
        }
    }
}
