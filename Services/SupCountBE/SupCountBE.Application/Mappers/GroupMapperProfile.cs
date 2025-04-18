
using SupCountBE.Application.Responses.Group;

namespace SupCountBE.Application.Mappers
{
    public class GroupMapperProfile : Profile
    {
        public GroupMapperProfile()
        {
            this.CreateMap<Group, GroupResponse>();
        }
    }
    
}
