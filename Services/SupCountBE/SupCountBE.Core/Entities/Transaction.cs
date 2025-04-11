

namespace SupCountBE.Core.Entities
{
    public  class Transaction

    {
        public int Id { get; set; }
        public required  string ReimbursementId { get; set; }
        public required  string PaymentMethod { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public Reimbursement? Reimbursement { get; set; }
    }
}
