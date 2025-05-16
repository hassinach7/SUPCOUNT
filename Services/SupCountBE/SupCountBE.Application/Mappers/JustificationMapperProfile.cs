using SupCountBE.Application.Responses.Justification;

namespace SupCountBE.Application.Mappers
{
    public class JustificationMapperProfile : Profile
    {
        public JustificationMapperProfile()
        {
            CreateMap<Justification, JustificationResponse>()
                .ForMember(dest => dest.ExpenseTitle, opt => opt.MapFrom(src => src.Expense!.Title))
                .ForMember(dest => dest.FileContent, opt => opt.MapFrom(src => Convert.ToBase64String(src.FileContent)));
        }
    }
}
