using SupCountBE.Application.Responses.User;
using SupCountFE.MVC.ViewModels.User;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse?> GetUserByIdAsync(string id);
        Task<UserResponse?> CreateUserAsync(RegisterUserVM model);
        Task<bool> UpdateUserAsync(UpdateUserVM model);

    }
}
