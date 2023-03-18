#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Options
 * 唯一标识：cb38eb9a-a2ca-4853-abb7-f5c3303b68db
 * 文件名：AzureSmsServiceOptions
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/18 13:54:39
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MasterAbp.Options
{
    /// <summary>
    /// AzureSmsServiceOptions 的摘要说明
    /// </summary>
    public class AzureSmsServiceOptions
    {

        public string Sender { get; set; }
        public string ConnectionString { get; set; }
    }
}