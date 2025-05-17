using SupCountBE.Application.Responses.UserGroup;

namespace SupCountBE.Application.Mappers
{
    public class UserGroupMapperProfile : Profile
    {
        public UserGroupMapperProfile()
        {
            CreateMap<UserGroup, UserGroupResponse>()
                 .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group!.Name))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User!.FullName));
               
        }
    }
    
}
