
using SupCountBE.Application.Responses.Expense;

namespace SupCountBE.Application.Mappers;

public class ExpenseMapperProfile : Profile
{
    public ExpenseMapperProfile()
    {
        CreateMap<Expense, ExpenseResponse>()
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group!.Name))
            .ForMember(dest => dest.ParticipationCount, opt => opt.MapFrom(src => src.Participations!.Count))
            .ForMember(dest => dest.JustificationCount, opt => opt.MapFrom(src => src.Justifications!.Count))
            .ReverseMap();
    }
}
