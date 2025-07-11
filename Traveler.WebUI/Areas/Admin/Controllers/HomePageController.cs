using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/HomePage")]
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
