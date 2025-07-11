using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Areas.Admin.ViewComponents.AdminUILayoutViewComponents
{
    [ViewComponent(Name = "AdminHeaderUILayout")]
    public class _HeaderUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
