#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 命名空间：MasterAbp.Products
 * 唯一标识：ed239584-cad5-4f1c-9798-8199743ae2ab
 * 文件名：CreateUpdateProductDto
 * 
 * 创建者：Robin
 * 电子邮箱：
 * 创建时间：2023/3/13 16:56:20
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
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MasterAbp.Products
{
    /// <summary>
    /// CreateUpdateProductDto 的摘要说明
    /// </summary>
    public class CreateUpdateProductDto
    {
        #region <属性>
        public Guid CategoryId { get; set; }
        [Required]
        [StringLength(ProductConsts.MaxNameLength)]
        public string Name { get; set; }
        public float Price { get; set; }
        public bool IsFreeCargo { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ProductStockState StockState { get; set; }
        #endregion <属性>

        #region <构造方法和析构方法>
        #endregion <构造方法和析构方法>

    }
}