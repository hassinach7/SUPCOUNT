using SupCountBE.Application.Queries.Participation;
using SupCountBE.Application.Responses.Participation;
using SupCountBE.Core.Repositories;

public class GetParticipationByIdHandler : IRequestHandler<GetParticipationByIdQuery, ParticipationResponse>
{
    private readonly IParticipationRepository _participationRepository;
    private readonly IMapper _mapper;
   

    public GetParticipationByIdHandler(IParticipationRepository participationRepository, IMapper mapper )
    {
        _participationRepository = participationRepository;
        _mapper = mapper;
       
    }

    public async Task<ParticipationResponse> Handle(GetParticipationByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _participationRepository.GetCurrentUser();

        var participation = await _participationRepository.GetByIdsAsync(request.ExpenseId);

        if (participation == null)
            throw new Exception("Participation not found.");

        return _mapper.Map<ParticipationResponse>(participation);
    }
}
