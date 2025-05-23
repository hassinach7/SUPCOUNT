using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace SupCountFE.MVC.Models;

public class ApiSecurity
{
    public HttpClient Http { get; private set; }
    public ApiSecurity(IOptions<ApiSetting> options)
    {
        Http = new HttpClient();
        Http.BaseAddress = new Uri(options.Value.Url ?? throw new Exception("Api Url is Null"));
        var httpContextAccessor = new HttpContextAccessor();
        if(httpContextAccessor.HttpContext == null)
        {
            return;
        }
        if (httpContextAccessor.HttpContext.Session == null)
        {
            return;
        }
        var token = httpContextAccessor!.HttpContext!.Session.GetString("JWTToken");
        if (!string.IsNullOrEmpty(token))
        {
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
