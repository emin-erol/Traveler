using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
