using SupCountBE.Application.Queries.Reimbursement;
using SupCountBE.Application.Responses.Reimbursement;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Reimbursement
{
    public class GetReimbursementByIdHandler : IRequestHandler<GetReimbursementByIdQuery, ReimbursementResponse>
    {
        private readonly IReimbursementRepository _repository;
        private readonly IMapper _mapper;

        public GetReimbursementByIdHandler(IReimbursementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReimbursementResponse> Handle(GetReimbursementByIdQuery request, CancellationToken cancellationToken)
        {
            var reimbursement = await _repository.GetByIdIncludingAsync(
                request.Id,
                includeSender: true,
                includeBeneficiary: true,
                includeGroup: true,
                includeTransactions: true
            );

            if (reimbursement == null)
                throw new Exception("Reimbursement not found.");

            return _mapper.Map<ReimbursementResponse>(reimbursement);
        }
    }
}
