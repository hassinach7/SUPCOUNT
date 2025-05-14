using SupCountFE.MVC.Models;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IAuthService
    {
        Task<AuthModel?> LoginAsync(LoginVM loginvm);
    }
}
