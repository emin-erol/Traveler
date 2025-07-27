using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.CarViewModels;
using Traveler.ViewModel.CityViewModels;
using Traveler.ViewModel.LocationAvailabilityViewModels;
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

            var response = await client.GetAsync("https://localhost:7252/api/Locations/GetLocationWithCityAndAvailability");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetLocationWithCityAndAvailabilityViewModel>>(jsonData);

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
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationRequestViewModel request)
        {
            var dto = request.Dto;
            var laDtos = request.LaDtos;

            var client = _httpClientFactory.CreateClient();

            var locationJson = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(locationJson, Encoding.UTF8, "application/json");

            var locationResponse = await client.PostAsync("https://localhost:7252/api/Locations", content);

            if(locationResponse.IsSuccessStatusCode)
            {
                var locationData = await locationResponse.Content.ReadAsStringAsync();
                var createdLocation = JsonConvert.DeserializeObject<ResultLocationViewModel>(locationData)!;

                int newLocationId = createdLocation.LocationId;

                if (laDtos != null)
                {
                    foreach (var la in laDtos)
                    {
                        la.LocationId = newLocationId;

                        var laJson = JsonConvert.SerializeObject(la);
                        StringContent laContent = new StringContent(laJson, Encoding.UTF8, "application/json");

                        await client.PostAsync("https://localhost:7252/api/LocationAvailabilities", laContent);
                    }
                }

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Location") });
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
            var laResponse = await client.GetAsync("https://localhost:7252/api/LocationAvailabilities/GetAllLocationAvailabilitiesByLocationId/" + locationId);

            if (locationResponse.IsSuccessStatusCode &&
                citiesResponse.IsSuccessStatusCode &&
                laResponse.IsSuccessStatusCode)
            {
                var locationJson = await locationResponse.Content.ReadAsStringAsync();
                var citiesJson = await citiesResponse.Content.ReadAsStringAsync();
                var laJson = await laResponse.Content.ReadAsStringAsync();

                var locationValue = JsonConvert.DeserializeObject<UpdateLocationViewModel>(locationJson);
                var citiesValues = JsonConvert.DeserializeObject<List<ResultCityViewModel>>(citiesJson);
                var laValues = JsonConvert.DeserializeObject<List<ResultLocationAvailabilityViewModel>>(laJson);

                ViewBag.Cities = citiesValues;
                ViewBag.LocationAvailabilities = laValues;

                return View(locationValue);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateLocation")]
        public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationRequestViewModel request)
        {
            var dto = request.Dto;
            var laDtos = request.LaDtos;

            var client = _httpClientFactory.CreateClient();

            var locationJson = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(locationJson, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Locations/", content);

            if (response.IsSuccessStatusCode)
            {
                if(laDtos != null)
                {
                    foreach(var laDto in  laDtos)
                    {
                        var laDtoJson = JsonConvert.SerializeObject(laDto);
                        StringContent laContent = new StringContent(laDtoJson, Encoding.UTF8, "application/json");

                        await client.PutAsync("https://localhost:7252/api/LocationAvailabilities/", laContent);
                    }
                }

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Location") });
            }
            

            return View("UpdateLocationModal", dto);
        }
    }
}
