using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminUILayout")]
    public class AdminUILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
