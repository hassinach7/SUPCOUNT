using SupCountBE.Application.Queries.UserGroup;

namespace SupCountBE.Application.Mappers
{
    public class UserGroupMapperProfile : Profile
    {
        public UserGroupMapperProfile()
        {
            this.CreateMap<UserGroup, UserGroupResponse>();
        }
    }
    
}
