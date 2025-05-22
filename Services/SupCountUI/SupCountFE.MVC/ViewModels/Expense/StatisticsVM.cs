namespace SupCountFE.MVC.ViewModels.Expense
{
    public class StatisticsVM
    {
        public decimal TotalAmount { get; set; }
        public Dictionary<string, decimal> AmountByCategory { get; set; } = new();
        public Dictionary<string, decimal> AmountByMonth { get; set; } = new();
        public Dictionary<string, decimal> AmountByGroup { get; set; } = new();
    }
}
