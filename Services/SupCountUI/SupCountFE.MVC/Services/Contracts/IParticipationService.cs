using SupCountBE.Application.Responses.Participation;
using SupCountFE.MVC.ViewModels.Participation;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IParticipationService
    {
      
         Task<IEnumerable<ParticipationResponse>> GetAllParticipationsAsync();
         Task<ParticipationResponse> CreateParticipationAsync(CreateParticipationVM model);
       

        
    }
}
