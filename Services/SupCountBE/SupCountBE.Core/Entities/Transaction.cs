

namespace SupCountBE.Core.Entities;

public  class Transaction : BaseEntity

{
    public required  int ReimbursementId { get; set; }
    public required  string PaymentMethod { get; set; }
    public float Amount { get; set; }
    public Reimbursement? Reimbursement { get; set; }
}
