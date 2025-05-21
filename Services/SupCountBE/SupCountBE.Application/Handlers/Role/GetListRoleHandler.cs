using SupCountBE.Application.Queries.Role;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Role;

public class GetListRoleHandler : IRequestHandler<GetListRoleQuery, IList<string?>>
{
    private readonly IRoleRepository _roleRepository;
    public GetListRoleHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<IList<string?>> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetListAsync();
        return roles;
    }
}
