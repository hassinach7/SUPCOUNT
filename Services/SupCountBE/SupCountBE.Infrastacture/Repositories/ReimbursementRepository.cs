using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;

namespace SupCountBE.Infrastacture.Repositories;

public class ReimbursementRepository : AsyncRepository<Reimbursement>, IReimbursementRepository
{
    public ReimbursementRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<IList<Reimbursement>> GetAllListIncludingAsync(bool includeSender = false, bool includeBeneficiary = false, bool includeGroup = false)
    {
        var query = _dbContext.Reimbursements.AsQueryable();

        if (includeSender)
            query = query.Include(r => r.Sender);

        if (includeBeneficiary)
            query = query.Include(r => r.Beneficiary);

        if (includeGroup)
            query = query.Include(r => r.Group);

        return await query.ToListAsync();
    }

        public async Task<Reimbursement?> GetByIdIncludingAsync(
        int id,
        bool includeSender = false,
        bool includeBeneficiary = false,
        bool includeGroup = false,
        bool includeTransactions = false)
    {
        var query = _dbContext.Reimbursements.AsQueryable();

        if (includeSender)
        {
            query = query.Include(r => r.Sender);
        }

        if (includeBeneficiary)
        {
            query = query.Include(r => r.Beneficiary);
        }

        if (includeGroup)
        {
            query = query.Include(r => r.Group);
        }

        if (includeTransactions)
        {
            query = query.Include(r => r.Transactions);
        }

        return await query.SingleOrDefaultAsync(r => r.Id == id);
    }
}
