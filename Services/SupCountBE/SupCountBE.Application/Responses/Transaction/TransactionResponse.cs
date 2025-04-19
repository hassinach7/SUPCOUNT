
namespace SupCountBE.Application.Responses.Transaction
{
    public class TransactionResponse 
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public float Amount { get; set; }
        public string ReimbursementName { get; set; } = null!;
    }
}
