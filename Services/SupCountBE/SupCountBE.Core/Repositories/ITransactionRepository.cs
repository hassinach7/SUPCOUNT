namespace SupCountBE.Core.Repositories
{
    public interface ITransactionRepository : IAsyncRepository<Transaction>
    {

        Task<Transaction?> GetByIdIncludingAsync(
       int id,
       bool includeReimbursement = false
   );
        Task<IList<Transaction>> GetAllListIncludingAsync(
       bool includeReimbursement = false
   );
    }
}
