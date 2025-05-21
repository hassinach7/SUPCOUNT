

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
    public bool IncludeSenders { get; set; } = false;
    public bool IncludeBeneficiaries { get; set; } = false;
    public bool IncludeGroups { get; set; } = false;
    public bool IncludeTransactions { get; set; } = false;
}
