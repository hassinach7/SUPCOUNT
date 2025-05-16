using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Expense;

namespace SupCountFE.MVC.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;
        private readonly IGroupService _groupService;
        private readonly ICategoryService _categoryService;
        private readonly IJustificationService justificationService;


        [HttpGet]
        public IActionResult Dwonload( string Base64String)
        {

            try
            {
                var fileBytes = Convert.FromBase64String(Base64String);

                return File(fileBytes, "application/octet-stream", "Justification");
            }
            catch (FormatException)
            {
                return BadRequest("Invalid base64 string format.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Justifications(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var justifications = await justificationService.GetListAsync(id.Value);
            return View(_mapper.Map<IList<JustificationExepnseVM>>(justifications));
        }
        public ExpenseController(IExpenseService expenseService, IMapper mapper, IGroupService groupService,
            ICategoryService categoryService, IJustificationService justificationService)
        {
            _expenseService = expenseService;
            _mapper = mapper;
            _groupService = groupService;
            _categoryService = categoryService;
            this.justificationService = justificationService;
        }

        // GET: /Expense/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();
            return View(_mapper.Map<List<ExpenseVM>>(expenses));
        }

        // GET: /Expense/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateExpenseVM();
            model = await FillListe(model);

            return View(model);
        }

        private async Task<CreateExpenseVM> FillListe(CreateExpenseVM model)
        {
            var groups = await _groupService.GetAllGroupsAsync();
            var categories = await _categoryService.GetAllAsync();

            model.GroupsItems = new SelectList(groups, "Id", "Name");
            model.CategoriesItems = new SelectList(categories, "Id", "Name");

            return model;
        }


        // POST: /Expense/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateExpenseVM model)
        {
            if (!ModelState.IsValid)
            {
                model = await FillListe(model);
                return View(model);
            }

            var result = await _expenseService.CreateExpenseAsync(model);
            if (result != null)
            {
                TempData["Success"] = "Expense created successfully!";
                return RedirectToAction(nameof(List));
            }

            ModelState.AddModelError("", "Failed to create expense.");

            var g = await _groupService.GetAllGroupsAsync();
            var c = await _categoryService.GetAllAsync();
            ViewBag.Groups = new SelectList(g, "Id", "Name");
            ViewBag.Categories = new SelectList(c, "Id", "Name");
            return View(model);
        }


        // GET: /Expense/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
                return NotFound();

            var model = _mapper.Map<UpdateExpenseVM>(expense);
            return View(model);
        }

        // POST: /Expense/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateExpenseVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _expenseService.UpdateExpenseAsync(model);
            TempData["Success"] = "Expense updated successfully!";
            return RedirectToAction(nameof(List));
        }

        // GET: /Expense/Participate/{id}
        [HttpGet]
        public IActionResult Participate(int id)
        {
            TempData["Info"] = $"Participation logic to be implemented for expense {id}.";
            return RedirectToAction(nameof(List));
        }


    }
}

