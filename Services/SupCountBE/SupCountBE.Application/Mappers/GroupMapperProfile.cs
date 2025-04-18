
using SupCountBE.Application.Responses.Group;

namespace SupCountBE.Application.Mappers
{
    public class GroupMapperProfile : Profile
    {
        public GroupMapperProfile()
        {
            CreateMap<Group, GroupResponse>()
                .ForMember(dest => dest.UserGroupsCount, opt => opt.MapFrom(src => src.UserGroups!.Count))
                .ForMember(dest => dest.ExpenseCount, opt => opt.MapFrom(src => src.Expenses!.Count))
                .ForMember(dest => dest.ReimbursementCount, opt => opt.MapFrom(src => src.Reimbursements!.Count))
                .ForMember(dest => dest.MessageCount, opt => opt.MapFrom(src => src.Messages!.Count))
                .ReverseMap();

        }
    }
    
}
