using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;


namespace SupCountBE.Infrastacture.Repositories;

public class UserGroupRepository : AsyncRepository<UserGroup>, IUserGroupRepository
{

    public async Task<IList<UserGroup>> GetListIncludingAsync(bool includeUser = false, bool includeGroup = false)
    {
        var query = _dbContext.UserGroups.AsQueryable();

        if (includeUser)
            query = query.Include(ug => ug.User);

        if (includeGroup)
            query = query.Include(ug => ug.Group);

        return await query.ToListAsync();
    }

    public UserGroupRepository(SupCountDbContext dbContext) : base(dbContext) { }
     public async Task<UserGroup?> GetByIdsAsync( int groupId)
    {
        return await _dbContext.UserGroups
            .Include(ug => ug.User)
            .Include(ug => ug.Group)
            .FirstOrDefaultAsync(ug =>  ug.GroupId == groupId);
    }

    public async Task<UserGroup?> GetByIdsIncludingAsync(

        int groupId,
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

        return await query.SingleOrDefaultAsync(ug =>  ug.GroupId == groupId);
    }

    public async Task<IList<UserGroup>> GetListByGroupIdAsync(int groupId, bool includeUser = false)
    {
        var query = _dbContext.UserGroups.AsQueryable();

        if (includeUser)
            query = query.Include(ug => ug.User);

        return await query.Where(ug => ug.GroupId == groupId).ToListAsync();
    }

    public Task<UserGroup?> GetByUserIdAndGroupIdAsync(int userId, int groupId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserInGroup(int userId, int groupId)
    {
        throw new NotImplementedException();
    }
}
