#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：Payment.Admin.HttpApi
 * 唯一标识：68ba105c-9ca6-4f15-8a2d-09470c25f89a
 * 文件名：PaymentAdminController
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/6/28 19:26:21
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

using Payment.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Payment.Admin
{
    public abstract class PaymentAdminController : AbpControllerBase
    {
        protected PaymentAdminController()
        {
            LocalizationResource = typeof(PaymentResource);
        }
    }
}