using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace SupCountBE.Infrastacture.Repositories;

public class GroupRepository : AsyncRepository<Group>, IGroupRepository
{
    public GroupRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<Group?> GetByIdIncludingAsync(
        int id,
        bool includeUserGroups = false,
        bool includeExpenses = false,
        bool includeReimbursements = false,
        bool includeMessages = false)
    {
        var query = _dbContext.Groups.AsQueryable();

        if (includeUserGroups) query = query.Include(g => g.UserGroups);
        if (includeExpenses) query = query.Include(g => g.Expenses);
        if (includeReimbursements) query = query.Include(g => g.Reimbursements);
        if (includeMessages) query = query.Include(g => g.Messages);

        return await query.SingleOrDefaultAsync(g => g.Id == id);
    }
}
