
using SupCountBE.Application.Queries.Reimbursement;
using SupCountBE.Application.Responses.Reimbursement;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Reimbursement;

public class GetAllReimbursementHandler : IRequestHandler<GetAllReimbursementQuery, IList<ReimbursementResponse>>
{
    private readonly IReimbursementRepository _reimbursementRepository;
    private readonly IMapper _mapper;

    public GetAllReimbursementHandler(IReimbursementRepository reimbursementRepository, IMapper mapper)
    {
        _reimbursementRepository = reimbursementRepository;
        _mapper = mapper;
    }

    public async Task<IList<ReimbursementResponse>> Handle(GetAllReimbursementQuery request, CancellationToken cancellationToken)
    {
        var reimbursements = await _reimbursementRepository.ListAllAsync();
        return _mapper.Map<IList<ReimbursementResponse>>(reimbursements);
    }
}
