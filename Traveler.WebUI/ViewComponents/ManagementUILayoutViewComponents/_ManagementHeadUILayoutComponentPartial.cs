using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.ViewComponents.ManagementUILayoutViewComponents
{
    public class _ManagementHeadUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
