using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;
using SupCountBE.Infrastacture.Repositories;

namespace SupCountBE.Infrastructure.Repositories;

public class ReimbursementRepository : AsyncRepository<Reimbursement>, IReimbursementRepository
{
    public ReimbursementRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<Reimbursement?> GetByIdIncludingAsync(int id, ReimbursementIncludingProperties reimbursementIncludingProperties)
    {
        var query = Get(reimbursementIncludingProperties);
        return await query.SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IList<Reimbursement>> GetAllListIncludingAsync(ReimbursementIncludingProperties reimbursementIncludingProperties)
    {
        var query = Get(reimbursementIncludingProperties);
        return await query.ToListAsync();
    }

    private IQueryable<Reimbursement> Get(ReimbursementIncludingProperties props)
    {
        var query = _dbContext.Reimbursements.AsQueryable();

        if (props.IncludeSenders)
        {
            query = query.Include(r => r.Sender);
        }

        if (props.IncludeBeneficiaries)
        {
            query = query.Include(r => r.Beneficiary);
        }

        if (props.IncludeGroups)
        {
            query = query.Include(r => r.Group);
        }

        if (props.IncludeTransactions)
        {
            query = query.Include(r => r.Transactions);
        }

        return query;
    }
}
