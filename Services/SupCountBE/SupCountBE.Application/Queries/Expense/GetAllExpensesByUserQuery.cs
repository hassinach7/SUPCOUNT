using SupCountBE.Application.Responses.Expense;

namespace SupCountBE.Application.Queries.Expense
{
    public class GetAllExpensesByUserQuery : IRequest<List<ExpenseResponse>>
    {
        public string UserId { get; set; } = string.Empty;
    }
  
}
