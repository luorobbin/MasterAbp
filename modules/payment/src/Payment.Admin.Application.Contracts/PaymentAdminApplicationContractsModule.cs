#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：Payment.Admin.Application.Contracts
 * 唯一标识：08c10dc6-77f9-4821-8759-42f4c63ce115
 * 文件名：PaymentAdminApplicationContractsModule
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/6/28 19:12:59
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

using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Payment.Admin
{
    [DependsOn(
        typeof(PaymentDomainSharedModule),
        typeof(AbpDddApplicationContractsModule)
        )]
    public class PaymentAdminApplicationContractsModule : AbpModule
    {

    }
}