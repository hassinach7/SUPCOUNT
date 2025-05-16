using AutoMapper;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Group;

namespace SupCountFE.MVC.Controllers
{
   
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupController(IGroupService groupService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _groupService = groupService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: /Groupe/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var data = await _groupService.GetAllGroupsAsync();
            return View(data);
        }

        // GET: /Group/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateGroupVM());
        }

        // POST: /Group/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _groupService.CreateGroupAsync(model);
            if (result != null)
            {
                TempData["Success"] = "Group created successfully!";
                return RedirectToAction(nameof(List));
            }

            ModelState.AddModelError("", "Failed to create group.");
            return View(model);
        }

        // GET: /Group/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var groupeDto = await _groupService.GetGroupByIdAsync(id);
            if (groupeDto == null)
                return NotFound();

            var model = _mapper.Map<UpdateGroupVM>(groupeDto);
            return View(model);
        }

        // POST: /Group/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateGroupVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _groupService.UpdateGroupAsync(model);
            TempData["Success"] = "Group updated successfully!";
            return RedirectToAction(nameof(List));
        }

     
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound();

            return PartialView("_GroupDetailsPartial", group);
        }


    }
}


