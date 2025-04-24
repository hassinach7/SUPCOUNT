using SupCountBE.Application.Queries.UserGroup;
using SupCountBE.Application.Responses.UserGroup;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.UserGroup
{
    public class GetUserGroupByIdHandler : IRequestHandler<GetUserGroupByIdQuery, UserGroupResponse>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;

        public GetUserGroupByIdHandler(IUserGroupRepository userGroupRepository, IMapper mapper)
        {
            _userGroupRepository = userGroupRepository;
            _mapper = mapper;
        }

        public async Task<UserGroupResponse> Handle(GetUserGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userGroupRepository.GetCurrentUser();
            var userGroup = await _userGroupRepository.GetByIdsIncludingAsync(
              
                request.GroupId,
                includeUser: true,
                includeGroup: true
            );

            if (userGroup == null)
                throw new Exception("UserGroup not found.");

            return _mapper.Map<UserGroupResponse>(userGroup);
        }
    }
}
