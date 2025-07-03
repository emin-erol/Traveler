using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
