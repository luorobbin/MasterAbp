using MasterAbp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MasterAbp;

[DependsOn(
    typeof(MasterAbpEntityFrameworkCoreTestModule)
    )]
public class MasterAbpDomainTestModule : AbpModule
{

}
