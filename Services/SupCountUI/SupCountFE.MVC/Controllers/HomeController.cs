namespace SupCountFE.MVC.Controllers
{
	[Route("[controller]")]
    public class HomeController : Controller
	{
      
        public IActionResult Index()
		{
			return View();
		}
	}
}
