#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Users
 * 唯一标识：1c875147-2d85-4ec2-8f9b-620eda614bec
 * 文件名：MyUserService
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/30 19:42:26
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

using MasterAbp.Caching;
using System.Threading.Tasks;
using System;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Volo.Abp.Application.Services;

namespace MasterAbp.Users
{
    /// <summary>
    /// MyUserService 的摘要说明
    /// </summary>
    public class MyUserService : ILocalEventHandler<EntityChangedEventData<IdentityUser>>, ITransientDependency, IApplicationService
    {
        private readonly IDistributedCache<UserCacheItem> _userCache;
        private readonly IRepository<IdentityUser, Guid> _userRepository;

        public MyUserService(IDistributedCache<UserCacheItem> userCache,
            IRepository<IdentityUser, Guid> userRepository)
        {
            _userCache = userCache;
            _userRepository = userRepository;
        }

        public async Task<UserCacheItem> GetUserInfoAsync(Guid userId)
        {
            return await _userCache.GetOrAddAsync(
                userId.ToString(),
                async () => await GetUserFromDatabaseAsync(userId),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                });
        }

        private async Task<UserCacheItem> GetUserFromDatabaseAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            return new UserCacheItem
            {
                Id = user.Id,
                EmailAddress = user.Email,
                UserName = user.UserName
            };
        }

        public async Task HandleEventAsync(EntityChangedEventData<IdentityUser> eventData)
        {
            await _userCache.RemoveAsync(eventData.Entity.Id.ToString());
        }

    }
}