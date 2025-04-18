using SupCountBE.Application.Queries.Group;
using SupCountBE.Application.Responses.Group;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Group;

public class GetAllGroupHandler : IRequestHandler<GetAllGroupQuery, IList<GroupResponse>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GetAllGroupHandler(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<IList<GroupResponse>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetAllListIncludingAsync(includeUserGroups: true, includeExpenses: true, includeReimbursements: true, includeMessages: true);
        return _mapper.Map<IList<GroupResponse>>(groups);
    }
}
