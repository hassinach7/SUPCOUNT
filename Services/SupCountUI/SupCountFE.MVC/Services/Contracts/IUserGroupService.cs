using SupCountBE.Application.Responses.UserGroup;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IUserGroupService
    {
        Task<UserGroupResponse> JoinGroupAsync(int groupId, string role);
        Task<IEnumerable<UserGroupResponse>> GetAllAsync();
    }
}
