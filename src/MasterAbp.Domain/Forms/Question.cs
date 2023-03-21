#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Forms
 * 唯一标识：418a70c5-706a-4542-a1f3-022e604e8667
 * 文件名：Question
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/18 14:35:35
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
    /// Question 的摘要说明
    /// </summary>
    public class Question : Entity<Guid>
    {
        public Guid FormId { get; set; }
        public string Title { get; set; }
        public bool AllowMultiSelect { get; set; }
        //public ICollection<Option> Options { get; set; }
    }
}