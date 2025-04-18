using SupCountBE.Application.Responses.Transaction;

namespace SupCountBE.Application.Queries.Transaction
{
    public class GetAllTransactionQuery : IRequest<List<TransactionResponse>>
    {
        public GetAllTransactionQuery()
        {
        }
    }
    
}
