using Newtonsoft.Json;
using SupCountBE.Application.Responses.Reimbursement;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Reimbursement;
using System.Text;

namespace SupCountFE.MVC.Services.Implementations
{
    public class ReimbursementService : IReimbursementService
    {
        private readonly ApiSecurity _apiSecurity;

        public ReimbursementService(ApiSecurity apiSecurity)
        {
            _apiSecurity = apiSecurity;
        }

        public async Task<bool> CreateReimbursementAsync(CreateReimbursementVM model)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("Reimbursement/Create", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<ReimbursementVM>> GetAllReimbursementsAsync()
        {
            var response = await _apiSecurity.Http.GetAsync("Reimbursement/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<ReimbursementResponse>>();
                return data?.Select(r => new ReimbursementVM
                {
                    Id = r.Id,
                    Name = r.Name,
                    Amount = r.Amount,
                    SenderName = r.SenderName,
                    BeneficiaryName = r.BeneficiaryName,
                    GroupName = r.GroupName,
                    TransactionCount = r.TransactionCount
                }) ?? Enumerable.Empty<ReimbursementVM>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<ReimbursementVM?> GetReimbursementByIdAsync(int id)
        {
            var response = await _apiSecurity.Http.GetAsync($"Reimbursement/GetById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadFromJsonAsync<ReimbursementResponse>();
                if (r == null) return null;

                return new ReimbursementVM
                {
                    Id = r.Id,
                    Name = r.Name,
                    Amount = r.Amount,
                    SenderName = r.SenderName,
                    BeneficiaryName = r.BeneficiaryName,
                    GroupName = r.GroupName,
                    TransactionCount = r.TransactionCount
                };
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }
        public async Task<List<ReimbursementVM>> GenerateReimbursementsAsync(int groupId)
        {
            var response = await _apiSecurity.Http.GetAsync($"Reimbursement/Generate?groupId={groupId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            var data = await response.Content.ReadFromJsonAsync<List<ReimbursementVM>>();
            return data ?? new List<ReimbursementVM>();
        }

    }
}
