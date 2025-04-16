using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;


namespace SupCountBE.Infrastacture.Repositories;

public class UserGroupRepository : AsyncRepository<UserGroup>, IUserGroupRepository
{
    public UserGroupRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<UserGroup?> GetByIdsIncludingAsync(
        string userId,
        string groupId,
        bool includeUser = false,
        bool includeGroup = false)
    {
        var query = _dbContext.UserGroups.AsQueryable();

        if (includeUser)
        {
            query = query.Include(ug => ug.User);
        }

        if (includeGroup)
        {
            query = query.Include(ug => ug.Group);
        }

        return await query.SingleOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);
    }
}
