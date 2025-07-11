using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Traveler.WebUI.Areas.Admin.ViewComponents.AdminUILayoutViewComponents
{
    [ViewComponent(Name = "AdminNavbarUILayout")]
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.UserName = UserClaimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;

            return View();
        }
    }
}
