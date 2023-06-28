using MasterAbp.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FluentValidation;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace MasterAbp;

[DependsOn(
    typeof(MasterAbpDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(MasterAbpApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpFluentValidationModule)
    )]
public class MasterAbpApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MasterAbpApplicationModule>();
        });

        var cofiguration = context.Services.GetConfiguration();
        Configure<AzureSmsServiceOptions>(cofiguration.GetSection("AzureSmsServiceOptions"));

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.GlobalCacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
        });

        Configure<AbpSignalROptions>(options =>
        {
            options.Hubs.AddOrUpdate(
            typeof(MessagingHub),
            config => //Additional configuration
            {
                config.RoutePattern = "/the-messaging-hub";
                config.ConfigureActions.Add(hubOptions =>
                {
                    hubOptions.LongPolling.PollTimeout =
                        TimeSpan.FromSeconds(30);
                });
            });
        });
    }
}
