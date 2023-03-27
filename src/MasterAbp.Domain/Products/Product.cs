using MasterAbp.Categories;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MasterAbp.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool IsFreeCargo { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ProductStockState StockState { get; set; }

        public Product()
        {            
        }

        public Product(Guid categoryId, string name, float price, bool isFreeCargo
            , DateTime releaseDate, ProductStockState stockState)
        {
            CategoryId = categoryId;
            Name = name;
            Price = price;
            IsFreeCargo = isFreeCargo;
            ReleaseDate = releaseDate;
            StockState = stockState;
        }

    }
}
