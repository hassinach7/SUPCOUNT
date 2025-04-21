using SupCountBE.Application.Queries.UserGroup;
using SupCountBE.Application.Responses.UserGroup;
using SupCountBE.Core.Repositories;


namespace SupCountBE.Application.Handlers.UserGroup
{
    public class GetAllUserGroupHandler : IRequestHandler<GetAllUserGroupQuery, IList<UserGroupResponse>>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;

        public GetAllUserGroupHandler(IUserGroupRepository userGroupRepository, IMapper mapper)
        {
            _userGroupRepository = userGroupRepository;
            _mapper = mapper;
        }

        public async Task<IList<UserGroupResponse>> Handle(GetAllUserGroupQuery request, CancellationToken cancellationToken)
        {
            var userGroups = await _userGroupRepository.GetListIncludingAsync(includeUser: true, includeGroup: true);
            return _mapper.Map<IList<UserGroupResponse>>(userGroups);
        }
    }

    
}
