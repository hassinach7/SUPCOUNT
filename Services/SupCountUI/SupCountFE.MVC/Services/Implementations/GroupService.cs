using Newtonsoft.Json;
using SupCountBE.Application.Responses.Group;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Group;
using System.Text;

namespace SupCountFE.MVC.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly ApiSecurity _apiSecurity;
   

        public GroupService(ApiSecurity apiSecurity)
        {
            _apiSecurity = apiSecurity;
           
        }

        public async Task<RetournCreatedGroupVM?> CreateGroupAsync(CreateGroupVM model)
        {

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
            var response = await _apiSecurity.Http.GetAsync($"Group/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupResponse>();
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> UpdateGroupAsync(UpdateGroupVM model)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PutAsync("Group/Edit", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }
       
        
    }
}
