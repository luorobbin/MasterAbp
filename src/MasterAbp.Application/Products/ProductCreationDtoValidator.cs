#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Products
 * 唯一标识：e4cd4e5e-70e6-4464-9c8b-49e727b9ec8c
 * 文件名：ProductCreationDtoValidator
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/25 18:32:58
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

using FluentValidation;


namespace MasterAbp.Products
{
    /// <summary>
    /// ProductCreationDtoValidator 的摘要说明
    /// </summary>
    public class ProductCreationDtoValidator : AbstractValidator<CreateUpdateProductDto>
    {
        public ProductCreationDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).ExclusiveBetween(0, 1000);
        }
    }
}