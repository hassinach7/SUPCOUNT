using SupCountBE.Application.Responses.Category;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Category;
using System.Net.Http.Headers;

namespace SupCountFE.MVC.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ApiSecurity _apiSecurity;
        private readonly Helper _helper;

        public CategoryService(ApiSecurity apiSecurity, Helper helper)
        {
            _apiSecurity = apiSecurity;
            _helper = helper;
        }

        public async Task<CategoryResponse?> CreateAsync(CreateCategoryVM model)
        {
            AddAuthHeader();

            var response = await _apiSecurity.Http.PostAsJsonAsync("category/Create", model);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<CategoryResponse>();
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            AddAuthHeader();

            var response = await _apiSecurity.Http.GetAsync("category/GetAll");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<IEnumerable<CategoryResponse>>() ?? [];
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            AddAuthHeader();

            var response = await _apiSecurity.Http.GetAsync($"category/GetById?id={id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<CategoryResponse>();
        }

        public async Task UpdateAsync(UpdateCategoryVM model)
        {
            AddAuthHeader();

            var response = await _apiSecurity.Http.PutAsJsonAsync("category/Edit", model);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        private void AddAuthHeader()
        {
            if (!string.IsNullOrEmpty(_helper.JWTToken))
            {
                _apiSecurity.Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _helper.JWTToken);
            }
        }
    }
}
