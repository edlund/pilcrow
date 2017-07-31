using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pilcrow.Core.Helpers;
using Pilcrow.Db;
using Pilcrow.Db.Migrations;
using Pilcrow.Db.Models;
using Pilcrow.Db.Models.Globalization;
using Pilcrow.Db.Repositories;
using Pilcrow.Db.Repositories.Cms;
using Pilcrow.Services;

namespace Pilcrow.Mvc
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        
        public List<IStartable> Startables { get; }
        
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
            Startables = (
                from startable in TypeHelper.GetImplementingTypes(typeof(IStartable))
                select (IStartable)Activator.CreateInstance(startable)
            ).ToList();
            
            Entity.RegisterClassMaps();
        }
        
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            var context = new Context();
            context.Connect(
                Configuration["Database:ConnectionString"],
                Configuration["Database:DatabaseName"]
            );
            
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IContext>(context);
            
            services.AddScoped<IPageRepository, PageRepository>();
            
            services.AddScoped<IPageService, PageService>();
            
            Startables.ForEach(x => x.ConfigureServices(Configuration, services));
            
            new MigrationRunner(context, Configuration).Execute();
        }
        
        public virtual void Configure(
            IApplicationBuilder applicationBuilder,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            if (hostingEnvironment.IsDevelopment()) {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseBrowserLink();
            } else {
                applicationBuilder.UseExceptionHandler("/Home/Error");
            }
            
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseMvc(routeBuilder =>
            {
                Startables.ForEach(x => x.ConfigureRoutes(
                    Configuration,
                    applicationBuilder,
                    hostingEnvironment,
                    routeBuilder
                ));
                
                routeBuilder.MapRoute(
                    name: "*Controller",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                routeBuilder.MapRoute(
                    name: "PathController",
                    template: "{culture}/{*path}",
                    defaults: new {
                        controller = "Path",
                        action = "View"
                    },
                    constraints: new {
                        culture = new RegexRouteConstraint(Translatable.CultureRegex)
                    }
                );
            });
            
            Startables.ForEach(x => x.Configure(
                Configuration,
                applicationBuilder,
                hostingEnvironment,
                loggerFactory
            ));
        }
    }
}
