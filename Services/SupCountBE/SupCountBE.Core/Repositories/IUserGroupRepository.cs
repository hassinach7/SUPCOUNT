namespace SupCountBE.Core.Repositories;

public interface IUserGroupRepository : IAsyncRepository<UserGroup>
{
    Task<UserGroup?> GetByIdsIncludingAsync(
      
        int groupId,
        bool includeUser = false,
        bool includeGroup = false
    );

    Task<UserGroup?> GetByIdsAsync( int groupId);
    Task<IList<UserGroup>> GetListIncludingAsync(
    bool includeUser = false,
    bool includeGroup = false
);
    Task<IList<UserGroup>> GetListByGroupIdAsync(
        int groupId,
        bool includeUser = false
    );
    Task<UserGroup?> GetByUserIdAndGroupIdAsync(int userId, int groupId);
   
}
