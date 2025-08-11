using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Traveler.ViewModel.CarViewModels;
using Traveler.ViewModel.LocationViewModels;
using Traveler.ViewModel.MileagePackageViewModels;
using Traveler.ViewModel.ReservationViewModels;
using Traveler.ViewModel.SecurityPackageViewModels;

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
            var mileagePackageResponse = await client.GetAsync("https://localhost:7252/api/MileagePackages");
            var securityPackageResponse = await client.GetAsync("https://localhost:7252/api/SecurityPackages/GetAllSecurityPackagesWithPackageOptions");

            if (locationResponse.IsSuccessStatusCode &&
                mileagePackageResponse.IsSuccessStatusCode &&
                securityPackageResponse.IsSuccessStatusCode)
            {
                var locationJson = await locationResponse.Content.ReadAsStringAsync();
                var mileagePackageJson = await mileagePackageResponse.Content.ReadAsStringAsync();
                var securityPackageJson = await securityPackageResponse.Content.ReadAsStringAsync();

                var locationValues = JsonConvert.DeserializeObject<List<GetLocationWithCityAndAvailabilityViewModel>>(locationJson);
                var mileagePackageValues = JsonConvert.DeserializeObject<List<ResultMileagePackageViewModel>>(mileagePackageJson);
                var securityPackageValues = JsonConvert.DeserializeObject<List<GetSecurityPackagesWithPackageOptionsViewModel>>(securityPackageJson);

                ViewBag.Locations = locationValues;
                ViewBag.MileagePackages = mileagePackageValues;
                ViewBag.SecurityPackages = securityPackageValues;
                ViewBag.v1 = "Rezervasyon";
                ViewBag.v2 = "Rezervasyon İşlemi";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody]CreateReservationViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
