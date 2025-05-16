using Newtonsoft.Json;
using SupCountBE.Application.Responses.Group;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Group;
using System.Net.Http.Headers;
using System.Text;

namespace SupCountFE.MVC.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly ApiSecurity _apiSecurity;
        private readonly Helper _helper;

        public GroupService(ApiSecurity apiSecurity, Helper helper)
        {
            _apiSecurity = apiSecurity;
            _helper = helper;
        }

        public async Task<RetournCreatedGroupVM?> CreateGroupAsync(CreateGroupVM model)
        {
            AddAuthHeader();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("Group/Create", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RetournCreatedGroupVM>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<GroupResponse>> GetAllGroupsAsync()
        {
            AddAuthHeader();

            var response = await _apiSecurity.Http.GetAsync("group/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<GroupResponse>>();
                return data ?? Enumerable.Empty<GroupResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<GroupResponse?> GetGroupByIdAsync(int id)
        {
            AddAuthHeader();

            var response = await _apiSecurity.Http.GetAsync($"Group/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> UpdateGroupAsync(UpdateGroupVM model)
        {
            AddAuthHeader();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PutAsync("Group/Edit", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

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
