#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Forms
 * 唯一标识：54b36cc5-1d6f-465b-a345-f4d88894be0c
 * 文件名：Form
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/18 14:35:05
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
using Volo.Abp.Domain.Entities;

namespace MasterAbp.Forms
{
    /// <summary>
    /// Form 的摘要说明
    /// </summary>
    public class Form : BasicAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDraft { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}