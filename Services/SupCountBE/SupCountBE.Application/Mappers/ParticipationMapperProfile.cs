using SupCountBE.Application.Responses;

namespace SupCountBE.Application.Mappers;

    public class ParticipationMapperProfile : Profile
    {
        public ParticipationMapperProfile()
        {
            this.CreateMap<Participation, ParticipationResponse>();

        }
    }

