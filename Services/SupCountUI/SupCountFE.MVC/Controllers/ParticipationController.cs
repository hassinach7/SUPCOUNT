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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var participations = await _participationService.GetAllParticipationsByUserAsync();
            var viewModels = _mapper.Map<List<ParticipationVM>>(participations);
            return View(viewModels);
        }
      
    }
}
