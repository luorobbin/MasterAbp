#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp
 * 唯一标识：0160bd0f-0782-42fc-86c6-069d30480202
 * 文件名：MessagingHub
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/6/23 19:58:29
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

using Volo.Abp.AspNetCore.SignalR;

namespace MasterAbp
{
    /// <summary>
    /// MessagingHub 的摘要说明
    /// </summary>
    [HubRoute("/the-messaging-hub")]
    public class MessagingHub : AbpHub
    {
         
    }
}