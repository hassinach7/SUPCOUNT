using SupCountBE.Application.Queries.Message;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Message
{
    public class GetMessageByIdHandler : IRequestHandler<GetMessageByIdQuery, MessageResponse>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;

        public GetMessageByIdHandler(IMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MessageResponse> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var message = await _repository.GetByIdIncludingAsync(
                request.Id,
                includeSender: true,
                includeRecipient: true,
                includeGroup: true
            );

            if (message == null)
                throw new Exception("Message not found.");

            return _mapper.Map<MessageResponse>(message);
        }
    }
}
