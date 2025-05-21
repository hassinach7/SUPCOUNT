using SupCountFE.MVC.ViewModels.Reimbursement;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IReimbursementService
    {
        Task<IEnumerable<ReimbursementVM>> GetAllReimbursementsAsync();
        Task<ReimbursementVM?> GetReimbursementByIdAsync(int id);
        Task<bool> CreateReimbursementAsync(CreateReimbursementVM model);
        Task<List<ReimbursementVM>> GenerateReimbursementsAsync(int groupId);

        //Task<bool> UpdateReimbursementAsync(UpdateReimbursementVM model);

    }
}
