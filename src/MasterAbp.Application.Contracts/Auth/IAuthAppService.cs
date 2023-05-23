#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Auth
 * 唯一标识：b540fb20-d850-4c13-a6b9-380c57414b5c
 * 文件名：IAuthAppService
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/5/23 9:30:03
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MasterAbp.Auth
{
    /// <summary>
    /// IAuthAppService 的摘要说明
    /// </summary>
    public interface IAuthAppService : IApplicationService
    {
        Task GetToken();
    }
}