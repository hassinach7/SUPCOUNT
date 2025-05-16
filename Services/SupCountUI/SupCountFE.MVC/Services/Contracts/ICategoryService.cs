using SupCountBE.Application.Responses.Category;
using SupCountFE.MVC.ViewModels.Category;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> CreateAsync(CreateCategoryVM model);
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task UpdateAsync(UpdateCategoryVM model);

    }
}
