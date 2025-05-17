using AutoMapper;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.UserGroup;

namespace SupCountFE.MVC.Controllers
{
    public class UserGroupController : Controller
    {
        private readonly IUserGroupService _userGroupService;
        private readonly IMapper _mapper;

        public UserGroupController(IUserGroupService userGroupService, IMapper mapper)
        {
            _userGroupService = userGroupService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Join(int groupId)
        {
            try
            {
                // Tu peux changer "Member" par "Viewer", "Admin", etc.
                await _userGroupService.JoinGroupAsync(groupId, "Member");
                TempData["Success"] = "You joined the group successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to join the group: {ex.Message}";
            }

            return RedirectToAction("List", "Group");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var members = await _userGroupService.GetAllAsync();
            return View(_mapper.Map<List<UserGroupVM>>(members));
        }

    }
}

