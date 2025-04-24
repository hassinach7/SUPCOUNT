using AutoMapper;
using MediatR;
using SupCountBE.Application.Queries.Group;
using SupCountBE.Application.Responses.Group;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Group
{
    public class GetGroupByIdHandler : IRequestHandler<GetGroupByIdQuery, GroupResponse>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GetGroupByIdHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<GroupResponse> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdIncludingAsync(
                request.Id,
                includeUserGroups: true,
                includeExpenses: true,
                includeReimbursements: true,
                includeMessages: true
            );

            if (group == null)
                throw new Exception("Group not found.");

            return _mapper.Map<GroupResponse>(group);
        }
    }
}
