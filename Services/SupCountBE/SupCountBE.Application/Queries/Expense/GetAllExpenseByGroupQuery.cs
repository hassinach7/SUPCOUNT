using SupCountBE.Application.Responses.Expense;

namespace SupCountBE.Application.Queries.Expense;

public class GetAllExpenseByGroupQuery : IRequest<List<ExpenseResponse>>
{
    public int GroupId { get; set; }
}