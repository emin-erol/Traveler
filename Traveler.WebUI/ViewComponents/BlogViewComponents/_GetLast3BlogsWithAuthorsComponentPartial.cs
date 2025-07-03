using Microsoft.AspNetCore.Mvc;

namespace Traveler.WebUI.ViewComponents.BlogViewComponents
{
    public class _GetLast3BlogsWithAuthorsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
