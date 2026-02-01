using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.BrandViewModels;
using Traveler.ViewModel.CarClassViewModels;
using Traveler.ViewModel.CarViewModels;
using Traveler.ViewModel.LocationViewModels;
using Traveler.ViewModel.ModelViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Car")]
    public class CarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Cars/GetCarsWithBrandAndClass");

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetCarsWithBrandAndClassViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [Route("CarDetail/{carId}")]
        public async Task<IActionResult> CarDetail(int carId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Cars/GetCarWithAllDetails/" + carId);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetCarWithAllDetailsViewModel>(jsonData);

                return View(value);
            }

            return View();
        }

        [Route("CreateCarModal")]
        [HttpGet]
        public async Task<IActionResult> CreateCarModal()
        {
            var client = _httpClientFactory.CreateClient();

            var brandResponse = await client.GetAsync("https://localhost:7252/api/Brands");
            var carClassResponse = await client.GetAsync("https://localhost:7252/api/CarClasses");
            var locationsResponse = await client.GetAsync("https://localhost:7252/api/Locations");

            if (brandResponse.IsSuccessStatusCode &&
                carClassResponse.IsSuccessStatusCode &&
                locationsResponse.IsSuccessStatusCode)
            {
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();
                var locationsJson = await locationsResponse.Content.ReadAsStringAsync();

                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);
                var locationsValues = JsonConvert.DeserializeObject<List<ResultLocationViewModel>>(locationsJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClasses = carClassValues;
                ViewBag.Locations = locationsValues;

                return View();
            }

            return View();
        }

        [HttpPost]
        [Route("CreateCar")]
        public async Task<IActionResult> CreateCar(CreateCarViewModel dto)
        {
            dto.CreatedTime = DateTime.Now;
            dto.UpdatedTime = DateTime.Now;
            dto.LastUsedTime = DateTime.Now;

            var client = _httpClientFactory.CreateClient();

            var lastCarIdResponse = await client.GetAsync("https://localhost:7252/api/Cars/GetLastCarId");
            var lastCarIdJson = await lastCarIdResponse.Content.ReadAsStringAsync();
            var lastCarIdValue = JsonConvert.DeserializeObject<int>(lastCarIdJson);

            int newCarId = lastCarIdValue + 1;

            int idLength = newCarId.ToString().Length;

            int randomLength = 12 - idLength;

            Random random = new Random();
            string randomNumber = "";
            for (int i = 0; i < randomLength; i++)
            {
                randomNumber += random.Next(0, 10).ToString();
            }

            dto.StockNumber = randomNumber + newCarId.ToString();

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/Cars/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Car");
            }

            return View("CreateCarModal", dto);
        }


        [Route("UpdateCarModal/{carId}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCarModal(int carId)
        {
            var client = _httpClientFactory.CreateClient();

            var carResponse = await client.GetAsync("https://localhost:7252/api/Cars/" + carId);
            var brandResponse = await client.GetAsync("https://localhost:7252/api/Brands");
            var carClassResponse = await client.GetAsync("https://localhost:7252/api/CarClasses");
            var locationsResponse = await client.GetAsync("https://localhost:7252/api/Locations");

            if (carResponse.IsSuccessStatusCode &&
                brandResponse.IsSuccessStatusCode &&
                carClassResponse.IsSuccessStatusCode &&
                locationsResponse.IsSuccessStatusCode)
            {
                var carJson = await carResponse.Content.ReadAsStringAsync();
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();
                var locationsJson = await locationsResponse.Content.ReadAsStringAsync();

                var carValue = JsonConvert.DeserializeObject<UpdateCarViewModel>(carJson);
                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);
                var locationsValues = JsonConvert.DeserializeObject<List<ResultLocationViewModel>>(locationsJson);

                var selectedBrandResponse = await client.GetAsync("https://localhost:7252/api/Brands/GetBrandByModelId/" + carValue.ModelId);
                var selectedBrandJson = await selectedBrandResponse.Content.ReadAsStringAsync();
                var selectedBrandValue = JsonConvert.DeserializeObject<ResultBrandViewModel>(selectedBrandJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClasses = carClassValues;
                ViewBag.Locations = locationsValues;
                ViewBag.SelectedBrand = selectedBrandValue;

                return View(carValue);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateCar")]
        public async Task<IActionResult> UpdateCar(UpdateCarViewModel dto)
        {
            dto.UpdatedTime = DateTime.Now;

            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Cars/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Car");
            }

            return View("UpdateCarModal", dto);
        }

        [HttpGet("GetModelsOfBrand")]
        public async Task<IActionResult> GetModelsOfBrand(int brandId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Models/GetModelsByBrand/" + brandId);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var models = JsonConvert.DeserializeObject<List<GetModelsByBrandViewModel>>(json);
                return Json(models);
            }

            return StatusCode((int)response.StatusCode, "API hatası");
        }
    }
}
