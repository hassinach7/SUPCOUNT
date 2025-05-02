using SupCountBE.Application.Responses.Expense;

namespace SupCountBE.Application.Commands.Expense
{
    public class UpdateExpenseCommand : IRequest<Unit>
    {
        public int? Id { get; set; }
        public string Title { get; set; } = null!;
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        //public string PayerId { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? GroupId { get; set; }

    }
}
