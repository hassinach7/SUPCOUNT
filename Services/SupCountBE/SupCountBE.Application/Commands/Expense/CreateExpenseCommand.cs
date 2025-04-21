using SupCountBE.Application.Responses.Expense;

namespace SupCountBE.Application.Commands.Expense
{
    public class CreateExpenseCommand : IRequest<ExpenseResponse>
    {
        public string Title { get; set; } = null!;
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        //public string PayerId { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? GroupId { get; set; }

    }
    
}
