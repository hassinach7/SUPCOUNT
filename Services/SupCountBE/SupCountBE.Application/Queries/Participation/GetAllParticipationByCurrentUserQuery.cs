using SupCountBE.Application.Responses.Participation;

namespace SupCountBE.Application.Queries.Participation;

public class GetAllParticipationByCurrentUserQuery : IRequest<List<ParticipationResponse>>;