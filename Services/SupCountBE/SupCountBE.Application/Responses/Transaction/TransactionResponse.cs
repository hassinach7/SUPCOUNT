
namespace SupCountBE.Application.Responses.Transaction
{
    public class TransactionResponse 
    {
        public int Id { get; set; }
        public int ReimbursementId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public float Amount { get; set; }
    }
}
