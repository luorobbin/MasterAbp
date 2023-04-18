#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Caching
 * 唯一标识：587a0ca2-ec42-48a6-8e3d-80fdc0751251
 * 文件名：UserCacheItem
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/30 19:40:52
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
using Volo.Abp.MultiTenancy;

namespace MasterAbp.Caching
{
    /// <summary>
    /// UserCacheItem 的摘要说明
    /// </summary>
    //[IgnoreMultiTenancy]
    public class UserCacheItem
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}