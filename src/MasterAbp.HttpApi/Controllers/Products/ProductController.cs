#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Controllers.Products
 * 唯一标识：c6bf79ff-0caf-4be4-ab11-afb3e80233c9
 * 文件名：ProductController
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/22 19:20:54
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

using MasterAbp.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MasterAbp.Controllers.Products
{
    /// <summary>
    /// ProductController 的摘要说明
    /// </summary>
    [Route("product2")]
    public class Product2Controller : MasterAbpController, IProductAppService
    {
        private readonly IProductAppService _productAppService;
        private readonly IAuthorizationService _authorizationService;

        public Product2Controller(IProductAppService productAppService,
            IAuthorizationService authorizationService)
        {
            _productAppService = productAppService;
            _authorizationService = authorizationService;
        }

        [Authorize("ProductManagement.ProductCreation")]
        public async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
           return await _productAppService.CreateAsync(input);
        }

        //[Authorize("ProductManagement.ProductDeletion")]

        public async Task DeleteAsync(Guid id)
        {
            await _productAppService.DeleteAsync(id);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            return await _productAppService.GetAsync(id);
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
        {
            return await _productAppService.GetCategoriesAsync();
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await _productAppService.GetListAsync(input);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            await _productAppService.UpdateAsync(id, input);
        }
    }
}