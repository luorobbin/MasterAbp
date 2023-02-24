using System.Threading.Tasks;

namespace MasterAbp.Data;

public interface IMasterAbpDbSchemaMigrator
{
    Task MigrateAsync();
}
