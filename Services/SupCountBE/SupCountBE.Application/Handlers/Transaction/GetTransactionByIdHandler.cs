using SupCountBE.Application.Queries.Transaction;
using SupCountBE.Application.Responses.Transaction;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Transaction
{
    public class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, TransactionResponse>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetTransactionByIdHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransactionResponse> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdIncludingAsync(
                request.Id,
                includeReimbursement: true
            );

            if (transaction == null)
                throw new Exception("Transaction not found.");

            return _mapper.Map<TransactionResponse>(transaction);
        }
    }
}
