using SupCountBE.Application.Responses.Transaction;

namespace SupCountBE.Application.Commands.Transaction;

public class CreateTransactionCommand : IRequest<TransactionResponse>
{
    public int? ReimbursementId { get; set; } 
    public string PaymentMethod { get; set; } = null!;
    public float Amount { get; set; }
}
