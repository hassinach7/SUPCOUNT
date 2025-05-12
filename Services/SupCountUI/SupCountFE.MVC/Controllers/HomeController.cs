using Microsoft.AspNetCore.Mvc;

namespace SupCountFE.MVC.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}
	}
}
