using SupCountBE.Application.Responses.Expense;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Expense;
using System.Globalization;
using System.Net.Http.Headers;

namespace SupCountFE.MVC.Services.Implementations
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApiSecurity _api;
        private readonly Helper _helper;

        public ExpenseService(ApiSecurity api, Helper helper)
        {
            _api = api;
            _helper = helper;
        }

        public async Task<ReturnCreatedExpenseVM?> CreateExpenseAsync(CreateExpenseVM model, IFormFile justification)
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(model.Title), "Title");
            formData.Add(new StringContent(model.Amount.ToString(CultureInfo.InvariantCulture)), "Amount");
            formData.Add(new StringContent(model.Date.ToString("yyyy-MM-dd")), "Date");
            formData.Add(new StringContent(model.GroupId.ToString()), "GroupId");
            formData.Add(new StringContent(model.CategoryId.ToString()), "CategoryId");

            if (justification != null && justification.Length > 0)
            {
                var fileContent = new StreamContent(justification.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(justification.ContentType);
                formData.Add(fileContent, "files", justification.FileName); 
            }

            if (!string.IsNullOrEmpty(_helper.JWTToken))
            {
                _api.Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _helper.JWTToken);
            }

            var response = await _api.Http.PostAsync("expense/Create", formData);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReturnCreatedExpenseVM>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<ExpenseResponse>> GetAllExpensesAsync()
        {
            if (!string.IsNullOrEmpty(_helper.JWTToken))
            {
                _api.Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _helper.JWTToken);
            }

            var response = await _api.Http.GetAsync("expense/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<ExpenseResponse>>();
                return data ?? new List<ExpenseResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<ExpenseResponse?> GetExpenseByIdAsync(int id)
        {
            if (!string.IsNullOrEmpty(_helper.JWTToken))
            {
                _api.Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _helper.JWTToken);
            }

            var response = await _api.Http.GetAsync($"expense/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ExpenseResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> UpdateExpenseAsync(UpdateExpenseVM model)
        {
            if (!string.IsNullOrEmpty(_helper.JWTToken))
            {
                _api.Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _helper.JWTToken);
            }

            var response = await _api.Http.PutAsJsonAsync("expense/Edit", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            return true;
        }
    }
}

