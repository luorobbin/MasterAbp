using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MasterAbp.Data;
using Volo.Abp.DependencyInjection;

namespace MasterAbp.EntityFrameworkCore;

public class EntityFrameworkCoreMasterAbpDbSchemaMigrator
    : IMasterAbpDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMasterAbpDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MasterAbpDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MasterAbpDbContext>()
            .Database
            .MigrateAsync();
    }
}
