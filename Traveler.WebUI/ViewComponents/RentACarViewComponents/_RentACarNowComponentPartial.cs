using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.ViewComponents.RentACarViewComponents
{
    public class _RentACarNowComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
