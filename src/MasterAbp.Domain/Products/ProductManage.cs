#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Products
 * 唯一标识：5af4d3a1-0069-43dd-b869-5eb5401d7450
 * 文件名：ProductManage
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/27 19:24:31
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
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace MasterAbp.Products
{
    /// <summary>
    /// ProductManage 的摘要说明
    /// </summary>
    public class ProductManage : DomainService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductManage(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="IsFreeCargo"></param>
        /// <param name="releaseDate"></param>
        /// <param name="stockState"></param>
        /// <returns></returns>
        public async Task<Product> CreateAsync(Guid categoryId, string name, float price,
            bool isFreeCargo, DateTime releaseDate, ProductStockState stockState)
        {
            if(await _productRepository.AnyAsync(p => p.Name == name))
            {
                throw new BusinessException(MasterAbpDomainErrorCodes.ProductHasExisted).WithData("Name",name);
            }

            return new Product(categoryId, name, price, isFreeCargo, releaseDate, stockState);
        }



    }
}