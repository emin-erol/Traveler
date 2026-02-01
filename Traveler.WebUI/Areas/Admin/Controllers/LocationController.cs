using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.CarViewModels;
using Traveler.ViewModel.CityViewModels;
using Traveler.ViewModel.LocationViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Location")]
    public class LocationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LocationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Locations/GetLocationWithCity");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetLocationWithCityViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateLocationModal")]
        public async Task<IActionResult> CreateLocationModal()
        {
            var client = _httpClientFactory.CreateClient();

            var cityResponse = await client.GetAsync("https://localhost:7252/api/Cities");

            if (cityResponse.IsSuccessStatusCode)
            {
                var cityJson = await cityResponse.Content.ReadAsStringAsync();
                var cityValues = JsonConvert.DeserializeObject<List<ResultCityViewModel>>(cityJson);

                ViewBag.Cities = cityValues;
            }

            return View();
        }

        [HttpPost]
        [Route("CreateLocation")]
        public async Task<IActionResult> CreateLocation(CreateLocationViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var locationJson = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(locationJson, Encoding.UTF8, "application/json");

            var locationResponse = await client.PostAsync("https://localhost:7252/api/Locations", content);

            if(locationResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Location");
            }

            return View("CreateLocationModal", dto);
        }

        [HttpGet]
        [Route("UpdateLocationModal/{locationId}")]
        public async Task<IActionResult> UpdateLocationModal(int locationId)
        {
            var client = _httpClientFactory.CreateClient();

            var locationResponse = await client.GetAsync("https://localhost:7252/api/Locations/" + locationId);
            var citiesResponse = await client.GetAsync("https://localhost:7252/api/Cities");

            if (locationResponse.IsSuccessStatusCode &&
                citiesResponse.IsSuccessStatusCode)
            {
                var locationJson = await locationResponse.Content.ReadAsStringAsync();
                var citiesJson = await citiesResponse.Content.ReadAsStringAsync();

                var locationValue = JsonConvert.DeserializeObject<UpdateLocationViewModel>(locationJson);
                var citiesValues = JsonConvert.DeserializeObject<List<ResultCityViewModel>>(citiesJson);

                ViewBag.Cities = citiesValues;

                return View(locationValue);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateLocation")]
        public async Task<IActionResult> UpdateLocation(UpdateLocationViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var locationJson = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(locationJson, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Locations/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Location");
            }
            

            return View("UpdateLocationModal", dto);
        }
    }
}
