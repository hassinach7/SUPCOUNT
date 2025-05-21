using AutoMapper;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.User;

namespace SupCountFE.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IGroupService _groupService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IMapper mapper, IGroupService groupService,
            IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _groupService = groupService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(_mapper.Map<List<UserVM>>(users));
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterUserVM
            {
                Roles = await _roleService.GetRolesAsync(),
                SelectedRoles = new List<string>() 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = await _roleService.GetRolesAsync(); 
                return View(model);
            }

            try
            {
                model.Roles = model.SelectedRoles; 
                var createdUser = await _userService.CreateUserAsync(model);
                if (createdUser != null)
                {
                    TempData["Success"] = "User created successfully.";
                    return RedirectToAction(nameof(List));
                }

                ModelState.AddModelError("", "User creation failed.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }

            model.Roles = await _roleService.GetRolesAsync(); 
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return RedirectToAction(nameof(List));

            var userToUpdate = await _userService.GetUserByIdAsync(id);
            if (userToUpdate is null)
                return RedirectToAction(nameof(List));

            var model = _mapper.Map<UpdateUserVM>(userToUpdate);

        
            var allRoles = await _roleService.GetRolesAsync();

            var userRoles = userToUpdate.Roles ?? new List<string>();

            model.Roles = allRoles;
            model.SelectedRoles = userRoles;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserVM model)
        {
            if (!ModelState.IsValid)
            {
              
                model.Roles = await _roleService.GetRolesAsync();
                return View(model);
            }

            try
            {
                model.Roles = model.SelectedRoles;
                await _userService.UpdateUserAsync(model);

                TempData["Success"] = "User updated successfully.";
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                model.Roles = await _roleService.GetRolesAsync();
                return View(model);
            }
        }


        // GET: /GroupUser solde
        [HttpGet]
        public async Task<IActionResult> GetUserSoldesByGroupId(int groupId)
        {
            var userSoldes = await _userService.GetUserSoldesByGroupIdAsync(groupId);
            var group = await _groupService.GetGroupByIdAsync(groupId);

            ViewBag.GroupName = group?.Name ?? "Group";

            var viewModel = _mapper.Map<List<SoldeUserVM>>(userSoldes);
            return View("Balances", viewModel);
        }


    }

}

