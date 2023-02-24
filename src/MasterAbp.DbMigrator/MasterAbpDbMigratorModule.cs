using MasterAbp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MasterAbp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MasterAbpEntityFrameworkCoreModule),
    typeof(MasterAbpApplicationContractsModule)
    )]
public class MasterAbpDbMigratorModule : AbpModule
{

}
