using SupCountBE.Application.Responses.Participation;

namespace SupCountBE.Application.Commands.Participation
{
    public class CreateParticipationCommand : IRequest<ParticipationResponse>
    {
        public float Weight { get; set; }
        //public string UserId { get; set; } = null!;
        public int ExpenseId { get; set; }
    }
}
 