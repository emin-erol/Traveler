using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.ViewComponents.HomeViewComponents
{
    public class _HomeStatisticsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
