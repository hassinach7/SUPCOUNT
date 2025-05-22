using SupCountBE.Application.Responses.Expense;
using SupCountFE.MVC.ViewModels.Expense;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IExpenseService
    {
        Task<List<ExpenseResponse>> GetAllExpensesAsync();
        Task<ExpenseResponse?> GetExpenseByIdAsync(int id);
        Task<ReturnCreatedExpenseVM?> CreateExpenseAsync(CreateExpenseVM model);
        Task<bool> UpdateExpenseAsync(UpdateExpenseVM model);
        Task<StatisticsVM?> GetUserExpenseStatisticsAsync(string userId);
        Task<Stream?> ExportExpensesPdfAsync(int groupId);
        Task<Stream?> ExportExpensesCsvAsync(int groupId);



    }
}
