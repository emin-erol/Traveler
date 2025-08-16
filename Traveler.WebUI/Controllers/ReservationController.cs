using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
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
            var client = _httpClientFactory.CreateClient();

            viewModel.ReservationCode = GenerateRandomCode(10);
            viewModel.CreatedTime = DateTime.Now;
            viewModel.UpdatedTime = DateTime.Now;
            viewModel.Description = "aaaa";
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            viewModel.UserId = userId;

            var jsonData = JsonConvert.SerializeObject(viewModel);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7252/api/Reservations", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }

            return View("CreateReservationModal", viewModel);
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

        private string GenerateRandomCode(int length)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int idx = RandomNumberGenerator.GetInt32(chars.Length); // 0..chars.Length-1 arası güvenli int
                sb.Append(chars[idx]);
            }
            return sb.ToString();
        }
    }
}
