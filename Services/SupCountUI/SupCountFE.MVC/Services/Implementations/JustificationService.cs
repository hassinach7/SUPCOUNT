using SupCountBE.Application.Responses.Category;
using SupCountBE.Application.Responses.Justification;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;

namespace SupCountFE.MVC.Services.Implementations;

public class JustificationService : BaseService, IJustificationService
{
    public JustificationService(ApiSecurity apiSecurity, Helper helper) : base(apiSecurity, helper)
    {
    }
    public async Task<IEnumerable<JustificationResponse>> GetListAsync(int expenseId)
    {
        var response = await _apiSecurity.Http.GetAsync($"Justification/GetAll?ExpenseId={expenseId}");
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<JustificationResponse>>();

        return result == null ?[] : result;
    }
}
