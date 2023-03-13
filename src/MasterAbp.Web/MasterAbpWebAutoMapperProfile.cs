using AutoMapper;
using MasterAbp.Products;
using MasterAbp.Web.Pages.Products;

namespace MasterAbp.Web;

public class MasterAbpWebAutoMapperProfile : Profile
{
    public MasterAbpWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<CreateEditProductViewModel, CreateUpdateProductDto>();
        CreateMap<ProductDto, CreateEditProductViewModel>();
    }
}
