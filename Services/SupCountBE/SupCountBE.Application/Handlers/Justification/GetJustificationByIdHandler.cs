using SupCountBE.Application.Queries.Justification;
using SupCountBE.Application.Responses.Justification;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Justification
{
    public class GetJustificationByIdHandler : IRequestHandler<GetJustificationByIdQuery, JustificationResponse>
    {
        private readonly IJustificationRepository _repository;
        private readonly IMapper _mapper;

        public GetJustificationByIdHandler(IJustificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JustificationResponse> Handle(GetJustificationByIdQuery request, CancellationToken cancellationToken)
        {
            var justification = await _repository.GetByIdIncludingAsync(request.Id, includeExpense: true);

            if (justification == null)
                throw new Exception("Justification not found.");

            return _mapper.Map<JustificationResponse>(justification);
        }
    }
}
