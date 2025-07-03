using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.ViewComponents.HomeViewComponents
{
    public class _FeaturedVehiclesComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
