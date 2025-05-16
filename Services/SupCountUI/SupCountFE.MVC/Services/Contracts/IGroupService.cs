using SupCountBE.Application.Responses.Group;
using SupCountFE.MVC.ViewModels.Group;

namespace SupCountFE.MVC.Services.Contracts
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupResponse>> GetAllGroupsAsync();

        Task<GroupResponse?> GetGroupByIdAsync(int id);
        Task<RetournCreatedGroupVM?> CreateGroupAsync(CreateGroupVM model);
        Task <bool> UpdateGroupAsync(UpdateGroupVM model);

    }
}
