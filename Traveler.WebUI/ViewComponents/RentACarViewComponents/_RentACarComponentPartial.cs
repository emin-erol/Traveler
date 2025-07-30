using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.ViewComponents.RentACarViewComponents
{
    public class _RentACarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
