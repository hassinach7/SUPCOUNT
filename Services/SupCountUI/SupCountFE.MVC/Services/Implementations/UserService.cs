using Newtonsoft.Json;
using SupCountBE.Application.Responses.User;
using SupCountBE.Core.Entities;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.User;
using System.Text;

namespace SupCountFE.MVC.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApiSecurity _apiSecurity;

        public UserService(ApiSecurity apiSecurity)
        {
            _apiSecurity = apiSecurity;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {


            var response = await _apiSecurity.Http.GetAsync("User/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<IEnumerable<UserResponse>>();
                return users ?? Enumerable.Empty<UserResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<UserResponse?> GetUserByIdAsync(string id)
        {
            var response = await _apiSecurity.Http.GetAsync($"User/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<UserResponse?> CreateUserAsync(RegisterUserVM model)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("User/Register", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> UpdateUserAsync(UpdateUserVM model)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PutAsync($"User/Edit", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());

            }
        }

    }
}
