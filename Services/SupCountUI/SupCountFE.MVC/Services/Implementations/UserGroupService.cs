using Newtonsoft.Json;
using SupCountBE.Application.Responses.UserGroup;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.UserGroup;
using System.Text;

namespace SupCountFE.MVC.Services.Implementations
{
    public class UserGroupService : IUserGroupService
    {
        private readonly ApiSecurity _apiSecurity;

        public UserGroupService(ApiSecurity apiSecurity)
        {
            _apiSecurity = apiSecurity;
        }

        public async Task<IEnumerable<UserGroupResponse>> GetAllAsync()
        {
            var response = await _apiSecurity.Http.GetAsync("UserGroup/GetAll");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserGroupResponse>>()
                       ?? Enumerable.Empty<UserGroupResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }


        public async Task<UserGroupResponse> JoinGroupAsync(int groupId, string role)
        {
            var payload = new
            {
                GroupId = groupId,
                Role = role
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("UserGroup/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserGroupResponse>()
                    ?? throw new Exception("No response from the server.");
            }

            var rawError = await response.Content.ReadAsStringAsync();

            if (rawError.Contains("already") || rawError.Contains("duplicate") || rawError.Contains("PRIMARY KEY"))
            {
                throw new Exception("You are already a member of this group.");
            }

            throw new Exception("An error occurred while attempting to join the group.");
        }

    }
}
