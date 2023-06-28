#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：Payment.Admin.HttpApi
 * 唯一标识：e54ad49f-94be-4347-a13e-b4f373303444
 * 文件名：PaymentAdminHttpApiModule
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/6/28 19:25:00
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

using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Payment.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Payment.Admin
{
    [DependsOn(
       typeof(PaymentAdminApplicationContractsModule),
       typeof(AbpAspNetCoreMvcModule)
       )]
    public class PaymentAdminHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(PaymentAdminHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<PaymentResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}