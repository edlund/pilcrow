using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pilcrow.Db;
using Pilcrow.Db.Models;
using Pilcrow.Db.Repositories;
using Pilcrow.Db.Repositories.Cms;
using Pilcrow.Services;

namespace Pilcrow.Mvc
{
    public interface IStartable
    {
        void ConfigureServices(
            IConfigurationRoot configuration,
            IServiceCollection services);
        
        void Configure(
            IConfigurationRoot configuration,
            IApplicationBuilder applicationBuilder,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory);
    }
}
