using SupCountBE.Application.Responses.Expense;

namespace SupCountBE.Application.Queries.Expense;

public class GetExpenseByIdQuery(int id) : IRequest<ExpenseResponse>
{
    public int Id { get; set; } = id;
}