

namespace SupCountBE.Core.Repositories
{
    public interface IReimbursementRepository :  IAsyncRepository<Reimbursement>
    {
        Task<Reimbursement?> GetByIdIncludingAsync(
      int id,
        bool includeSender = false,
        bool includeBeneficiary = false,
        bool includeGroup = false,
        bool includeTransactions = false
    );
    }
}
