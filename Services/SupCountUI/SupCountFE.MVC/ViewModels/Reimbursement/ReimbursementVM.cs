namespace SupCountFE.MVC.ViewModels.Reimbursement
{
    public class ReimbursementVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Amount { get; set; }
        public string SenderName { get; set; } = null!;
        public string BeneficiaryName { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public int TransactionCount { get; set; }
    }
}
