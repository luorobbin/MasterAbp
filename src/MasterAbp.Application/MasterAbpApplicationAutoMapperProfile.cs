using AutoMapper;
using MasterAbp.Categories;
using MasterAbp.Products;

namespace MasterAbp;

public class MasterAbpApplicationAutoMapperProfile : Profile
{
    public MasterAbpApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Product, ProductDto>();
        CreateMap<Category, CategoryLookupDto>();
        CreateMap<CreateUpdateProductDto, Product>();
    }
}
