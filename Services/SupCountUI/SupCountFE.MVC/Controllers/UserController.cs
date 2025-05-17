using AutoMapper;
using SupCountBE.Application.Responses.User;
using SupCountFE.MVC.Services.Contracts;

using SupCountFE.MVC.ViewModels.User;

namespace SupCountFE.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(_mapper.Map<List<UserVM>>(users));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserVM());
        }

        // POST: /User/Create
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var createdUser = await _userService.CreateUserAsync(model);
                if (createdUser != null)
                {
                    TempData["Success"] = "User created successfully.";

                }
                ModelState.AddModelError("", "User creation failed.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(List));
            }

            // Récupérer l'utilisateur à modifier
            UserResponse? userToUpdate = await _userService.GetUserByIdAsync(id);
            if (userToUpdate is null)
            {
                return RedirectToAction(nameof(List));
            }

            var model = _mapper.Map<UpdateUserVM>(userToUpdate);
            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = model.Id;
            var userDto = _mapper.Map<UserResponse>(model);

            await _userService.UpdateUserAsync(model);

            return RedirectToAction("List", "User");
        }
        // GET: /GroupUser solde
        [HttpGet]
        public async Task<IActionResult> GetUserSoldesByGroupId(int groupId)
        {
            var userSoldes = await _userService.GetUserSoldesByGroupIdAsync(groupId);
            return View(userSoldes);
        }
    }

}

