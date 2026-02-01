using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Traveler.ViewModel.FeatureViewModels;
using Traveler.ViewModel.LocationViewModels;
using Traveler.ViewModel.MileagePackageViewModels;
using Traveler.ViewModel.PricingViewModels;
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

        [HttpGet]
        public async Task<IActionResult> CreateReservationModal()
        {
            var client = _httpClientFactory.CreateClient();

            var locationResponse = await client.GetAsync("https://localhost:7252/api/Locations/GetLocationWithCity");
            var mileagePackageResponse = await client.GetAsync("https://localhost:7252/api/MileagePackages");
            var securityPackageResponse = await client.GetAsync("https://localhost:7252/api/SecurityPackages/GetAllSecurityPackagesWithPackageOptions");
            var featuresResponse = await client.GetAsync("https://localhost:7252/api/Features");
            var pricingsResponse = await client.GetAsync("https://localhost:7252/api/Pricings");
            
            if (locationResponse.IsSuccessStatusCode &&
                mileagePackageResponse.IsSuccessStatusCode &&
                securityPackageResponse.IsSuccessStatusCode &&
                featuresResponse.IsSuccessStatusCode &&
                pricingsResponse.IsSuccessStatusCode)
            {
                var locationJson = await locationResponse.Content.ReadAsStringAsync();
                var mileagePackageJson = await mileagePackageResponse.Content.ReadAsStringAsync();
                var securityPackageJson = await securityPackageResponse.Content.ReadAsStringAsync();
                var featuresJson = await featuresResponse.Content.ReadAsStringAsync();
                var pricingsJson = await pricingsResponse.Content.ReadAsStringAsync();

                var locationValues = JsonConvert.DeserializeObject<List<GetLocationWithCityViewModel>>(locationJson);
                var mileagePackageValues = JsonConvert.DeserializeObject<List<ResultMileagePackageViewModel>>(mileagePackageJson);
                var securityPackageValues = JsonConvert.DeserializeObject<List<GetSecurityPackagesWithPackageOptionsViewModel>>(securityPackageJson);
                var featuresValues = JsonConvert.DeserializeObject<List<ResultFeatureViewModel>>(featuresJson);
                var pricingsValues = JsonConvert.DeserializeObject<List<ResultPricingViewModel>>(pricingsJson);

                ViewBag.Locations = locationValues;
                ViewBag.MileagePackages = mileagePackageValues;
                ViewBag.SecurityPackages = securityPackageValues;
                ViewBag.Features = featuresValues;
                ViewBag.Pricings = pricingsValues;
                ViewBag.v1 = "Rezervasyon";
                ViewBag.v2 = "Rezervasyon İşlemi";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequestViewModel request)
        {
            var client = _httpClientFactory.CreateClient();

            var reservation = request.Reservation;
            int modelId = request.ModelId;

            var pickUpDate = reservation.PickUpDate.ToString("yyyy-MM-dd");
            var dropOffDate = reservation.DropOffDate.ToString("yyyy-MM-dd");

            var carIdResponse = await client.GetAsync($"https://localhost:7252/api/Models/GetMostSuitableCarIdByModelId/{modelId}/{reservation.PickUpLocationId}/{pickUpDate}/{dropOffDate}");
            if (carIdResponse.IsSuccessStatusCode)
            {
                var carIdJson = await carIdResponse.Content.ReadAsStringAsync();
                var carId = JsonConvert.DeserializeObject<int>(carIdJson);

                reservation.CarId = carId;
                reservation.Description = "aaaaaa";
                reservation.ReservationCode = GenerateRandomCode(10);

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                reservation.UserId = userId;
            }

            var jsonData = JsonConvert.SerializeObject(reservation);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/Reservations", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }

            return BadRequest();
        }

        public async Task<IActionResult> GetAvailableModels(int locationId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7252/api/Models/GetAllModelsWithDetailsByLocation/{locationId}");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "API hatası");

            var jsonData = await response.Content.ReadAsStringAsync();
            return Content(jsonData, "application/json");
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int idx = RandomNumberGenerator.GetInt32(chars.Length);
                sb.Append(chars[idx]);
            }
            return sb.ToString();
        }
    }
}
