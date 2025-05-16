
using SupCountBE.Application.Queries.Justification;
using SupCountBE.Application.Responses.Justification;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Justification
{
    public class GetAllJustificationHandler : IRequestHandler<GetAllJustificationQuery, IList<JustificationResponse>>
    {
        private readonly IJustificationRepository _justificationRepository;
        private readonly IMapper _mapper;

        public GetAllJustificationHandler(IJustificationRepository justificationRepository, IMapper mapper)
        {
            _justificationRepository = justificationRepository;
            _mapper = mapper;
        }

        public async Task<IList<JustificationResponse>> Handle(GetAllJustificationQuery request, CancellationToken cancellationToken)
        {
            var justifications = await _justificationRepository.GetAllListByExpenseIdIncludingAsync(request.ExpenseId, true);
            return _mapper.Map<IList<JustificationResponse>>(justifications);
        }
    }
}
