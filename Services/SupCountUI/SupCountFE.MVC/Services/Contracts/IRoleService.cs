namespace SupCountFE.MVC.Services.Contracts;

public interface IRoleService
{
    Task<IList<string>> GetRolesAsync();
}
