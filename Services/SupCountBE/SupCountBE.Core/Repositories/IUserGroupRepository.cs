namespace SupCountBE.Core.Repositories;

public interface IUserGroupRepository : IAsyncRepository<UserGroup>
{
    Task<UserGroup?> GetByIdsIncludingAsync(
        string userId,
        int groupId,
        bool includeUser = false,
        bool includeGroup = false
    );
}
