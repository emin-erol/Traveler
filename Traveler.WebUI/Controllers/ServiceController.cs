using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "Hizmetler";
            ViewBag.v2 = "Sizin İçin Hizmetlerimiz";

            return View();
        }
    }
}
