using SupCountFE.MVC.Models;

namespace SupCountFE.MVC.Controllers
{
	[Route("[controller]")]
    public class HomeController : Controller
	{
        private readonly Helper helper;

        public HomeController(Helper helper)
        {
            this.helper = helper;
        }

        public IActionResult Index()
		{
            if(!helper.UserRoles.Contains("User"))
            {
                return RedirectToAction("List", "User");
            }
            return View();
		}
	}
}
