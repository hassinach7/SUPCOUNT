namespace SupCountBE.Core.Repositories;

public interface IUserGroupRepository : IAsyncRepository<UserGroup>
{
    Task<UserGroup?> GetByIdsIncludingAsync(
        string userId,
        int groupId,
        bool includeUser = false,
        bool includeGroup = false
    );

    Task<UserGroup?> GetByIdsAsync(string userId, int groupId);
    Task<IList<UserGroup>> GetListIncludingAsync(
    bool includeUser = false,
    bool includeGroup = false
);
}
