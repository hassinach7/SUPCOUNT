

namespace SupCountBE.Core.Repositories
{
    public interface IReimbursementRepository :  IAsyncRepository<Reimbursement>
    {
        Task<Reimbursement?> GetByIdIncludingAsync(
      int id, ReimbursementIncludingProperties reimbursementIncludingProperties);
        Task<IList<Reimbursement>> GetAllListIncludingAsync(ReimbursementIncludingProperties reimbursementIncludingProperties);
    }
}
public record ReimbursementIncludingProperties
{
    public bool IncludeSender { get; set; } = false;
    public bool IncludeBeneficiary { get; set; } = false;
    public bool IncludeGroup { get; set; } = false;
    public bool IncludeTransactions { get; set; } = false;
}
