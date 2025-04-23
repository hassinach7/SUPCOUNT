using SupCountBE.Application.Responses.Transaction;

namespace SupCountBE.Application.Commands.Transaction;

public class UpdateTransactionCommand : IRequest<TransactionResponse>
{
    public int Id { get; set; }

    public  int? ReimbursementId { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public float Amount { get; set; }
}
