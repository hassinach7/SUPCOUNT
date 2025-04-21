using SupCountBE.Application.Queries.Transaction;
using SupCountBE.Application.Responses.Transaction;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Transaction
{
    public class GetAllTransactionHandler : IRequestHandler<GetAllTransactionQuery, IList<TransactionResponse>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetAllTransactionHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IList<TransactionResponse>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetAllListIncludingAsync(includeReimbursement: true);
            return _mapper.Map<IList<TransactionResponse>>(transactions);
        }
    }
    
    
}
