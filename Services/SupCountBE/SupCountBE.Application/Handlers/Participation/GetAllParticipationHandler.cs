using SupCountBE.Application.Queries.Participation;
using SupCountBE.Application.Responses.Participation;
using SupCountBE.Core.Repositories;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SupCountBE.Application.Handlers.Participation
{
    public class GetAllParticipationHandler : IRequestHandler<GetAllParticipationQuery, IList<ParticipationResponse>>
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IMapper _mapper;

        public GetAllParticipationHandler(IParticipationRepository participationRepository, IMapper mapper)
        {
            _participationRepository = participationRepository;
            _mapper = mapper;
        }

        public async Task<IList<ParticipationResponse>> Handle(GetAllParticipationQuery request, CancellationToken cancellationToken)
        {
            var participations = await _participationRepository.GetListIncludingAsync(new ParticipationIncludingProperties
            {
                IncludeUsers = true,
                IncludeExpenses = true
            });

            return _mapper.Map<IList<ParticipationResponse>>(participations);
        }
    }
}
