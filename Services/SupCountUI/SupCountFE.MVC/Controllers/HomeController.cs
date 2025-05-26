using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly Helper helper;
    private readonly IExpenseService expenseService;

    public HomeController(Helper helper, IExpenseService expenseService)
    {
        this.helper = helper;
        this.expenseService = expenseService;
    }

    public async Task<IActionResult> Index()
    {
        if (!helper.UserRoles.Contains("User"))
        {
            return RedirectToAction("List", "User");
        }

        var expenses = await expenseService.GetUserExpenseStatisticsAsync(helper.UserId!);
        return View(expenses);
    }
}
