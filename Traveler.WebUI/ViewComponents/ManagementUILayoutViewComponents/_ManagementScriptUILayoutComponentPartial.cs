using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.ViewComponents.ManagementUILayoutViewComponents
{
    public class _ManagementScriptUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
