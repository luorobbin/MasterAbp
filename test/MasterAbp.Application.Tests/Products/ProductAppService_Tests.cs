#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Products
 * 唯一标识：5726d03b-7dd6-4920-b06a-110042ae797d
 * 文件名：ProductAppService_Tests
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/11 14:20:04
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

using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace MasterAbp.Products
{
    /// <summary>
    /// ProductAppService_Tests 的摘要说明
    /// </summary>
    public class ProductAppService_Tests : MasterAbpApplicationTestBase
    {
        private readonly IProductAppService _productAppService;

        public ProductAppService_Tests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
        }

        [Fact]
        public async Task Should_Get_Product_List()
        {
            //Act
            var output = await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            //Assert
            output.TotalCount.ShouldBe(3);
            output.Items.ShouldContain(x => x.Name.Contains("Acme Monochrome Laser Printer"));
        }
    }
}