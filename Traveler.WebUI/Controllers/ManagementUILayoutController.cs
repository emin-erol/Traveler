using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Controllers
{
    public class ManagementUILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
