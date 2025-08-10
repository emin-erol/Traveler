using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Traveler.ViewModel.CarViewModels;
using Traveler.ViewModel.LocationViewModels;
using Traveler.ViewModel.ReservationViewModels;

namespace Traveler.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAvailableCars(int locationId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7252/api/Cars/GetCarsWithAllDetailsByLocation/{locationId}");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "API hatası");

            var jsonData = await response.Content.ReadAsStringAsync();
            return Content(jsonData, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> CreateReservationModal()
        {
            var client = _httpClientFactory.CreateClient();

            var locationResponse = await client.GetAsync("https://localhost:7252/api/Locations/GetLocationWithCityAndAvailability");
            
            if (locationResponse.IsSuccessStatusCode)
            {
                var locationJson = await locationResponse.Content.ReadAsStringAsync();
                var locationValues = JsonConvert.DeserializeObject<List<GetLocationWithCityAndAvailabilityViewModel>>(locationJson);

                ViewBag.Locations = locationValues;
                ViewBag.v1 = "Rezervasyon";
                ViewBag.v2 = "Rezervasyon İşlemi";
            }

            return View();
        }
    }
}
