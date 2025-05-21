using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;

namespace SupCountFE.MVC.Services.Implementations;

public class RoleService(ApiSecurity _apiSecurity) : IRoleService
{
    public async Task<IList<string?>> GetRolesAsync()
    {

        var response = await _apiSecurity.Http.GetAsync("Role/GetAll");
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<IList<string?>>() ?? [];
    }
}
