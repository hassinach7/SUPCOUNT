using Newtonsoft.Json;
using SupCountBE.Application.Responses.Participation;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Participation;
using System.Text;

namespace SupCountFE.MVC.Services.Implementations
{
    public class ParticipationService : IParticipationService
    {


        private readonly ApiSecurity _apiSecurity;

        public ParticipationService(ApiSecurity apiSecurity)
        {
            _apiSecurity = apiSecurity;
        }

        public async Task<IEnumerable<ParticipationResponse>> GetAllParticipationsAsync()
        {
            var response = await _apiSecurity.Http.GetAsync("Participation/GetAll");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ParticipationResponse>>()
                       ?? Enumerable.Empty<ParticipationResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<ParticipationResponse> CreateParticipationAsync(CreateParticipationVM model)
        {
            var jsonPayload = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _apiSecurity.Http.PostAsync("Participation/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ParticipationResponse>()
                    ?? throw new Exception("No response from the server.");
            }

            var rawError = await response.Content.ReadAsStringAsync();

            if (rawError.Contains("already") || rawError.Contains("duplicate") || rawError.Contains("PRIMARY KEY"))
            {
                throw new Exception("Participation already exists or duplicates detected.");
            }

            throw new Exception("An error occurred while creating the participation.");
        }
    }
}

