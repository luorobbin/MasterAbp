#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Products
 * 唯一标识：cc8ab366-f6f7-4f27-b7e6-79e746fda8d0
 * 文件名：ProductAppService
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/11 13:57:42
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

using MasterAbp.Categories;
using MasterAbp.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MasterAbp.Products
{
    /// <summary>
    /// ProductAppService 的摘要说明
    /// </summary>
    public class ProductAppService : MasterAbpAppService, IProductAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        #region <常量>
        #endregion <常量>

        #region <变量>
        #endregion <变量>

        #region <属性>
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IOptions<AzureSmsServiceOptions> _options;

        #endregion <属性>

        #region <构造方法和析构方法>
        public ProductAppService(IRepository<Category,Guid> categoryRepository,
            IRepository<Product, Guid> productRepository,
            IOptions<AzureSmsServiceOptions> options)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _options = options;
        }

        #endregion <构造方法和析构方法>

        #region <方法>
        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _productRepository.WithDetailsAsync(x => x.Category);

            queryable = queryable
                        .Skip(input.SkipCount)
                        .Take(input.MaxResultCount)
                        .OrderBy(input.Sorting ?? nameof(Product.Name));

            var products = await AsyncExecuter.ToListAsync(queryable);
            var count = await _productRepository.GetCountAsync();

            return new PagedResultDto<ProductDto>(count,ObjectMapper.Map<List<Product>, List<ProductDto>>(products));
        }

        public async Task CreateAsync(CreateUpdateProductDto input)
        {
            await _productRepository.InsertAsync(ObjectMapper.Map<CreateUpdateProductDto, Product>(input));
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetListAsync();
            return new ListResultDto<CategoryLookupDto>(ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories));
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Product, ProductDto>(await _productRepository.GetAsync(id));
        }

        public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);
            ObjectMapper.Map(input, product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        #endregion <方法>

        #region <事件>
        #endregion <事件>
    }
}