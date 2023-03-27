#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Products
 * 唯一标识：400f4505-2cad-4c6e-b89d-5e0ef33ad04f
 * 文件名：IProductAppService
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/11 13:44:26
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
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MasterAbp.Products
{
    public interface IProductAppService: IApplicationService
    {
        Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<ProductDto> CreateAsync(CreateUpdateProductDto input);
        Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync();
        Task<ProductDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, CreateUpdateProductDto input);
        Task DeleteAsync(Guid id);
    }
}
