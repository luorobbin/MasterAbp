using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace MasterAbp;

public class MasterAbpWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<MasterAbpWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
