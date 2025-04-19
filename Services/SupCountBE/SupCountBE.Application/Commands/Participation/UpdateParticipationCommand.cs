

using SupCountBE.Application.Responses.Participation;

namespace SupCountBE.Application.Commands.Participation;

public class UpdateParticipationCommand : IRequest<ParticipationResponse>
{
    public string UserId { get; set; } = null!;
    public int ExpenseId { get; set; }
    public float Weight { get; set; }
}
