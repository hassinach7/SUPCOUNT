using SupCountBE.Application.Responses.Transaction;

namespace SupCountBE.Application.Queries.Transaction
{
    public class GetTransactionByIdQuery(int id) : IRequest<TransactionResponse>
    {
        public int Id { get; set; } = id;
    }
}
