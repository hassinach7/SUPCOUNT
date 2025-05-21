using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Reimbursement;

namespace SupCountFE.MVC.Controllers
{
    public class ReimbursementController : Controller
    {
        private readonly IReimbursementService _reimbursementService;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly Helper _helper;
        private readonly IMapper _mapper;

        public ReimbursementController(IReimbursementService reimbursementService, IMapper mapper,
            IGroupService groupService, IUserService userService, Helper helper)
        {
            _reimbursementService = reimbursementService;
            _mapper = mapper;
            _groupService = groupService;
            _userService = userService;
            _helper = helper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var reimbursements = await _reimbursementService.GetAllReimbursementsAsync();
            return View(_mapper.Map<List<ReimbursementVM>>(reimbursements));
        }

        // GET: /Reimbursement/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateReimbursementVM();
            model = await FillListe(model);
            return View(model);
        }

        private async Task<CreateReimbursementVM> FillListe(CreateReimbursementVM model)
        {
            var groups = await _groupService.GetAllGroupsAsync();
            var users = await _userService.GetAllUsersAsync();// ou GetAllUsersAsync()

            model.GroupsItems = new SelectList(groups, "Id", "Name");
            model.BeneficiariesItems = new SelectList(users.Where(o => o.Id != _helper.UserId).ToList(), "Id", "FullName");

            return model;
        }

        // POST: /Reimbursement/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateReimbursementVM model)
        {
            if (!ModelState.IsValid)
            {
                model = await FillListe(model);
                return View(model);
            }

            var result = await _reimbursementService.CreateReimbursementAsync(model);
            if (result)
            {
                TempData["Success"] = "Reimbursement created successfully!";
                return RedirectToAction(nameof(List));
            }

            ModelState.AddModelError("", "Failed to create reimbursement.");
            model = await FillListe(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Generate(int groupId)
        {
            try
            {
                var reimbursements = await _reimbursementService.GenerateReimbursementsAsync(groupId);
                ViewBag.GroupId = groupId;
                return View("Generated", reimbursements);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("List", "Group");
            }
        }

    }
}