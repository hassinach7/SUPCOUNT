using SupCountBE.Application.Responses.Participation;

namespace SupCountBE.Application.Mappers;

public class ParticipationMapperProfile : Profile
{
    public ParticipationMapperProfile()
    {
        CreateMap<Participation, ParticipationResponse>()
         .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.FullName))
         .ForMember(dest => dest.ExpenseTitle, opt => opt.MapFrom(src => src.Expense!.Title));

    }
}

