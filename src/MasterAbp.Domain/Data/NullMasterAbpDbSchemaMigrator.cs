using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MasterAbp.Data;

/* This is used if database provider does't define
 * IMasterAbpDbSchemaMigrator implementation.
 */
public class NullMasterAbpDbSchemaMigrator : IMasterAbpDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
