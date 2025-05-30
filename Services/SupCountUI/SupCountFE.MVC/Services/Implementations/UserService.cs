﻿using Newtonsoft.Json;
using SupCountBE.Application.Responses.User;
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
            if (model.SelectedRoles != null && model.SelectedRoles.Any())
            {
                model.Roles = model.SelectedRoles;
            }
            else
            {
                model.Roles = new List<string>();
            }

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
            if (model.SelectedRoles != null && model.SelectedRoles.Any())
            {
                model.Roles = model.SelectedRoles;
            }
            else
            {
                model.Roles = new List<string>();
            }
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
        public async Task<List<SoldeUserResponse>> GetUserSoldesByGroupIdAsync(int groupId)
        {
            var response = await _apiSecurity.Http.GetAsync($"User/GetAllUserSoldeByGroupId?groupId={groupId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<SoldeUserResponse>>();
                return data ?? new List<SoldeUserResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

    }
}
