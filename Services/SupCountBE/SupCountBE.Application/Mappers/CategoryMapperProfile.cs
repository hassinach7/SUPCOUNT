using SupCountBE.Application.Responses;

namespace SupCountBE.Application.Mappers;

public class CategoryMapperProfile : Profile
{
    public CategoryMapperProfile()
    {
        this.CreateMap<Category, CategoryResponse>();
    }
}
