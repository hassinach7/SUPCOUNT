using SupCountBE.Application.Responses.Justification;

namespace SupCountFE.MVC.Services.Contracts;

public interface IJustificationService
{
    Task<IEnumerable<JustificationResponse>> GetListAsync(int expenseId);
}
