using SupCountFE.MVC.Models;

namespace SupCountFE.MVC.Services.Implementations;

public class BaseService
{
    protected readonly ApiSecurity _apiSecurity;
    protected readonly Helper _helper;

    public BaseService(ApiSecurity apiSecurity, Helper helper)
    {
        _apiSecurity = apiSecurity;
        _helper = helper;
    }
}

