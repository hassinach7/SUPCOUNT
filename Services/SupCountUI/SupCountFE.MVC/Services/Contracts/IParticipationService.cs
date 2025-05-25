using SupCountBE.Application.Responses.Participation;

namespace SupCountFE.MVC.Services.Contracts;

public interface IParticipationService
{
     Task<IEnumerable<ParticipationResponse>> GetAllParticipationsByUserAsync();
     Task CreateParticipationAsync(int expenseId , float wieght);
}
