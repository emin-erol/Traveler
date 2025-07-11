using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Areas.Admin.ViewComponents.AdminUILayoutViewComponents
{
    [ViewComponent(Name = "AdminFooterUILayout")]
    public class _FooterUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
