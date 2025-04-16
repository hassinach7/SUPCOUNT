using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;


namespace SupCountBE.Infrastacture.Repositories;

public class TransactionRepository : AsyncRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(SupCountDbContext dbContext) : base(dbContext) { }

    public async Task<Transaction?> GetByIdIncludingAsync(
        int id,
        bool includeReimbursement = false)
    {
        var query = _dbContext.Transactions.AsQueryable();

        if (includeReimbursement)
        {
            query = query.Include(t => t.Reimbursement);
        }

        return await query.SingleOrDefaultAsync(t => t.Id == id);
    }
}
