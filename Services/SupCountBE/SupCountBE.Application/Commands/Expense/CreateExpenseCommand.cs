namespace SupCountBE.Application.Commands.Expense
{
    public class CreateExpenseCommand : IRequest<int>
    {
        public string? Title { get; set; } 
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        
        public int GroupId { get; set; }
      
    }
    
}
