#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.EntityFrameworkCore
 * 唯一标识：d4e76d15-c1b3-41d2-8d99-7a4c79f54ec4
 * 文件名：FormRepository
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/21 20:05:08
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

using MasterAbp.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MasterAbp.EntityFrameworkCore
{
    /// <summary>
    /// FormRepository 的摘要说明
    /// </summary>
    public class FormRepository : EfCoreRepository<MasterAbpDbContext, Form, Guid>, IFormRepository
    {
        public FormRepository(IDbContextProvider<MasterAbpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Form>> GetListAsync(string name, bool includeDrafts = false)
        {
            var context = await GetDbContextAsync();

            var query = context.Forms.Where(p => p.Name == name);

            if (!includeDrafts)
            {
                query = query.Where(p => !p.IsDraft);
            }

            return await query.ToListAsync();
        }
    }
}