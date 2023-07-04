using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Payment;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(PaymentDomainSharedModule)
)]
public class PaymentDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }

}
