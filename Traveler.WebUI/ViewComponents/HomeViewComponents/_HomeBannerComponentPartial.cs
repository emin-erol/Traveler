using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Traveler.ViewModel.BannerViewModels;

namespace Traveler.WebUI.ViewComponents.HomeViewComponents
{
    public class _HomeBannerComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeBannerComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7252/api/Banners");

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBannerViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }
    }
}
