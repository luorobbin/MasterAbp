#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Forms
 * 唯一标识：2175ed69-0ad0-41fa-9255-920e797cc98f
 * 文件名：IFormRepository
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/21 19:58:39
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
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MasterAbp.Forms
{
    /// <summary>
    /// IFormRepository 的摘要说明
    /// </summary>
    public interface IFormRepository : IRepository<Form, Guid>
    {
        Task<List<Form>> GetListAsync(string name, bool includeDrafts = false);
    }
}