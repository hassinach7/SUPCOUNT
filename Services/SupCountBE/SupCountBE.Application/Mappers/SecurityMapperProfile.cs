using SupCountBE.Application.Common.Models;
using SupCountBE.Application.Dtos;

namespace SupCountBE.Application.Mappers;

public class SecurityMapperProfile : Profile
{
    public SecurityMapperProfile()
    {
        CreateMap<AuthModel, LoginAuthResponseDto>();
    }
}
