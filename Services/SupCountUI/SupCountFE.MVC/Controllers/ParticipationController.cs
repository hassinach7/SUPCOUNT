using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.ViewModels.Participation;

namespace SupCountFE.MVC.Controllers
{
    public class ParticipationController : Controller
    {
        private readonly IParticipationService _participationService;
        private readonly IExpenseService _expenseService; 
        private readonly IMapper _mapper;

        public ParticipationController(IParticipationService participationService, IExpenseService expenseService, IMapper mapper)
        {
            _participationService = participationService;
            _expenseService = expenseService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateParticipationVM();
            model = await FillList(model); 
            return View(model);
        }

        private async Task<CreateParticipationVM> FillList(CreateParticipationVM model)
        {
            var expenses = await _expenseService.GetAllExpensesAsync(); 

         
            model.ExpensesItems = new SelectList(expenses, "Id", "Name");

            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateParticipationVM model)
        {
            if (!ModelState.IsValid)
            {
                model = await FillList(model);
                return View(model);
            }

            try
            {
                await _participationService.CreateParticipationAsync(model);
                TempData["Success"] = "Participation created successfully!";
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model = await FillList(model);
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var participations = await _participationService.GetAllParticipationsAsync();
            var viewModels = _mapper.Map<List<ParticipationVM>>(participations);
            return View(viewModels);
        }
    }
}
