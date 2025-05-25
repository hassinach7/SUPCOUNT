using SupCountBE.Application.Queries.Participation;
using SupCountBE.Application.Responses.Participation;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Participation;

public class GetAllParticipationByCurrentUserHandler : IRequestHandler<GetAllParticipationByCurrentUserQuery, List<ParticipationResponse>>
{
    private readonly IParticipationRepository _participationRepository;
    private readonly IMapper mapper;

    public GetAllParticipationByCurrentUserHandler(IParticipationRepository participationRepository, IMapper mapper)
    {
        _participationRepository = participationRepository;
        this.mapper = mapper;
    }
    public async Task<List<ParticipationResponse>> Handle(GetAllParticipationByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _participationRepository.GetCurrentUser();
        var participations = await _participationRepository.GetListByUserIdAsync(userId);
        return mapper.Map<List<ParticipationResponse>>(participations);
    }
}
