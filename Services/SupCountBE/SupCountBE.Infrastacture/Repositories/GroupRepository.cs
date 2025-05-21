using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;
using SupCountBE.Infrastacture.Repositories;

namespace SupCountBE.Infrastructure.Repositories;

public class GroupRepository : AsyncRepository<Group>, IGroupRepository
{
    public GroupRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<Group?> GetByIdIncludingAsync(int id, GroupIncludingProperties groupIncludingProperties)
    {
        var query = Get(groupIncludingProperties);
        return await query.SingleOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IList<Group>> GetAllListIncludingAsync(
        bool includeUserGroups = false,
        bool includeExpenses = false,
        bool includeReimbursements = false,
        bool includeMessages = false)
    {
        var props = new GroupIncludingProperties
        {
            IncludeUserGroups = includeUserGroups,
            IncludeExpenses = includeExpenses,
            IncludeReimbursements = includeReimbursements,
            IncludeMessages = includeMessages
        };

        var query = Get(props);
        return await query.ToListAsync();
    }

    private IQueryable<Group> Get(GroupIncludingProperties props)
    {
        var query = _dbContext.Groups.AsQueryable();

        if (props.IncludeUserGroups)
        {
            query = query.Include(g => g.UserGroups);
        }

        if (props.IncludeExpenses)
        {
            query = query.Include(g => g.Expenses);
        }

        if (props.IncludeReimbursements)
        {
            query = query.Include(g => g.Reimbursements);
        }

        if (props.IncludeMessages)
        {
            query = query.Include(g => g.Messages);
        }

        return query;
    }
}
