using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.Areas.Admin.ViewComponents.AdminUILayoutViewComponents
{
    [ViewComponent(Name = "AdminScriptUILayout")]
    public class _ScriptUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
