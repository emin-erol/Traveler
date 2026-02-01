using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Traveler.ViewModel.LocationViewModels;

namespace Traveler.WebUI.ViewComponents.RentACarViewComponents
{
    public class _ReservationComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ReservationComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Locations/GetLocationWithCityAndAvailability");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetLocationWithCityViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }
    }
}
