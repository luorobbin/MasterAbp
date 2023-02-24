using Volo.Abp.Modularity;

namespace MasterAbp;

[DependsOn(
    typeof(MasterAbpApplicationModule),
    typeof(MasterAbpDomainTestModule)
    )]
public class MasterAbpApplicationTestModule : AbpModule
{

}
