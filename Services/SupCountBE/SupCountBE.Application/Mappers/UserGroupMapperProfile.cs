using SupCountBE.Application.Responses.UserGroup;

namespace SupCountBE.Application.Mappers
{
    public class UserGroupMapperProfile : Profile
    {
        public UserGroupMapperProfile()
        {
            CreateMap<UserGroup, UserGroupResponse>()
                 .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group!.Name));
        }
    }
    
}
