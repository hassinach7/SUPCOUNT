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

        public async Task<IEnumerable<ParticipationResponse>> GetAllParticipationsByUserAsync()
        {
            var response = await _apiSecurity.Http.GetAsync("Participation/GetAllByUser");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ParticipationResponse>>()
                       ?? Enumerable.Empty<ParticipationResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task CreateParticipationAsync(int expenseId, float wieght)
        {
            var jsonPayload = JsonConvert.SerializeObject(new { weight = wieght, expenseId = expenseId });
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _apiSecurity.Http.PostAsync("Participation/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var rawError = await response.Content.ReadAsStringAsync(); // properyhy detail
            var error = JsonConvert.DeserializeObject<ErrorResponse>(rawError);
            throw new Exception(error?.Message ?? "An error occurred while creating the participation.");
        }

    }
    public class ErrorResponse
    {
        public string? Message { get; set; }
        public string? Details { get; set; }
    }
}

