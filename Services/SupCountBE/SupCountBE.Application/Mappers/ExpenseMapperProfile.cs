
using SupCountBE.Application.Responses.Expense;

namespace SupCountBE.Application.Mappers;

public class ExpenseMapperProfile : Profile
{
    public ExpenseMapperProfile()
    {
        CreateMap<Group, ExpenseGroupResponse>();

        CreateMap<Expense, ExpenseResponse>()
            .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
            .ForMember(dest => dest.Payer, opt => opt.MapFrom(src => src.Payer!.FullName))
            .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(dest => dest.ParticipationCount, opt => opt.MapFrom(src => src.Participations!.Count))
            .ForMember(dest => dest.JustificationCount, opt => opt.MapFrom(src => src.Justifications!.Count))
            .ReverseMap();
    }
}
