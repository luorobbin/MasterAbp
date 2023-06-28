#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：Payment.Admin.Application
 * 唯一标识：e00c5069-9f20-4cae-b2c6-c144e3d690ce
 * 文件名：PaymentAdminApplicationModule
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/6/28 19:08:49
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Payment.Admin
{
    [DependsOn(
        typeof(PaymentDomainModule),
        typeof(PaymentAdminApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class PaymentAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<PaymentAdminApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<PaymentAdminApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}