using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.BrandViewModels;
using Traveler.ViewModel.CarClassViewModels;
using Traveler.ViewModel.CarViewModels;

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

            if (brandResponse.IsSuccessStatusCode &&  carClassResponse.IsSuccessStatusCode)
            {
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();

                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClasses = carClassValues;

                return View();
            }

            return View();
        }

        [HttpPost]
        [Route("CreateContactInfoModal")]
        public async Task<IActionResult> CreateCar(CreateCarViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/Cars/", content);

            if( response.IsSuccessStatusCode)
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

            if (carResponse.IsSuccessStatusCode &&
                brandResponse.IsSuccessStatusCode &&
                carClassResponse.IsSuccessStatusCode)
            {
                var carJson = await carResponse.Content.ReadAsStringAsync();
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();

                var carValue = JsonConvert.DeserializeObject<UpdateCarViewModel>(carJson);
                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClasses = carClassValues;

                return View(carValue);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Cars/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Car");
            }

            return View("CreateCarModal", dto);
        }
    }
}
