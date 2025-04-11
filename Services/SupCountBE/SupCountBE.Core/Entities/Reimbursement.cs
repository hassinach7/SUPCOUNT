

namespace SupCountBE.Core.Entities
{
    public  class Reimbursement

    {
        public int Id { get; set; }
        public  required string Name { get; set; }
        public required string SenderId { get; set; }
        public User? Sender { get; set; }

        public required  string BeneficiaryId { get; set; }
        public User? Beneficiary { get; set; }
        public float Amount { get; set; }
        public  Group? group { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }

    }
}
