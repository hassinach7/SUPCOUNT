

using SupCountBE.Application.Responses.Justification;

namespace SupCountBE.Application.Queries.Justification;

public class GetAllJustificationQuery : IRequest<IList<JustificationResponse>>
{
    public GetAllJustificationQuery(int expenseId)
    {
        ExpenseId = expenseId;
    }

    public int ExpenseId { get; set; }
}
