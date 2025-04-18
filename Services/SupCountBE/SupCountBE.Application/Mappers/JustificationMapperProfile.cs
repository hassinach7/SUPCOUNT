using SupCountBE.Application.Responses.Justification;

namespace SupCountBE.Application.Mappers
{
    public class JustificationMapperProfile : Profile
    {
        public JustificationMapperProfile()
        {
            this.CreateMap<Justification, JustificationResponse>();
           
        }
    }
}
