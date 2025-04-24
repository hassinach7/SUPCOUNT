using SupCountBE.Application.Responses.Participation;

namespace SupCountBE.Application.Queries.Participation
{
    public class GetParticipationByIdQuery(int expenseId) : IRequest<ParticipationResponse>
    {
        public int ExpenseId { get; set; } = expenseId;
    }
}
