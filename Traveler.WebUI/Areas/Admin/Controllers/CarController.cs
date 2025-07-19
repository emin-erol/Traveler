using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.BrandViewModels;
using Traveler.ViewModel.CarClassViewModels;
using Traveler.ViewModel.CarFeatureViewModels;
using Traveler.ViewModel.CarViewModels;
using Traveler.ViewModel.FeatureViewModels;

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
            var featuresResponse = await client.GetAsync("https://localhost:7252/api/Features");

            if (brandResponse.IsSuccessStatusCode &&
                carClassResponse.IsSuccessStatusCode &&
                featuresResponse.IsSuccessStatusCode)
            {
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();
                var featuresJson = await featuresResponse.Content.ReadAsStringAsync();

                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);
                var featuresValues = JsonConvert.DeserializeObject<List<ResultFeatureViewModel>>(featuresJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClasses = carClassValues;
                ViewBag.Features = featuresValues;

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
                var responseData = await response.Content.ReadAsStringAsync();
                var createdCar = JsonConvert.DeserializeObject<ResultCarViewModel>(responseData)!;

                int newCarId = createdCar.CarId;

                if (dto.SelectedFeatureIds != null && dto.SelectedFeatureIds.Any())
                {
                    foreach (var featureId in dto.SelectedFeatureIds)
                    {
                        var carFeatureDto = new
                        {
                            CarId = newCarId,
                            FeatureId = featureId,
                            Available = true
                        };

                        var featureJson = JsonConvert.SerializeObject(carFeatureDto);
                        var featureContent = new StringContent(featureJson, Encoding.UTF8, "application/json");

                        await client.PostAsync("https://localhost:7252/api/CarFeatures", featureContent);
                    }
                }

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
            var featuresResponse = await client.GetAsync("https://localhost:7252/api/Features");
            var carFeaturesResponse = await client.GetAsync("https://localhost:7252/api/CarFeatures/GetCarFeaturesByCarId/" + carId);

            if (carResponse.IsSuccessStatusCode &&
                brandResponse.IsSuccessStatusCode &&
                carClassResponse.IsSuccessStatusCode &&
                featuresResponse.IsSuccessStatusCode &&
                carFeaturesResponse.IsSuccessStatusCode)
            {
                var carJson = await carResponse.Content.ReadAsStringAsync();
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();
                var featuresJson = await featuresResponse.Content.ReadAsStringAsync();
                var carFeaturesJson = await carFeaturesResponse.Content.ReadAsStringAsync();

                var carValue = JsonConvert.DeserializeObject<UpdateCarViewModel>(carJson);
                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);
                var featuresValues = JsonConvert.DeserializeObject<List<ResultFeatureViewModel>>(featuresJson);
                var carFeaturesValues = JsonConvert.DeserializeObject<List<ResultCarFeatureViewModel>>(carFeaturesJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClasses = carClassValues;
                ViewBag.Features = featuresValues;
                ViewBag.CarFeatures = carFeaturesValues;

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
                var carFeaturesResponse = await client.GetAsync("https://localhost:7252/api/CarFeatures/GetCarFeaturesByCarId/" + dto.CarId);
                var carFeaturesJson = await carFeaturesResponse.Content.ReadAsStringAsync();
                var carFeaturesValues = JsonConvert.DeserializeObject<List<ResultCarFeatureViewModel>>(carFeaturesJson);

                var currentFeatureIds = carFeaturesValues.Select(x => x.FeatureId).ToList();
                var selectedFeatureIds = dto.SelectedFeatureIds ?? new List<int>();

                var toAdd = selectedFeatureIds.Except(currentFeatureIds).ToList();

                foreach (var featureId in toAdd)
                {
                    var addContent = new StringContent(
                        JsonConvert.SerializeObject(new
                        {
                            CarId = dto.CarId,
                            FeatureId = featureId
                        }),
                        Encoding.UTF8,
                        "application/json");

                    await client.PostAsync("https://localhost:7252/api/CarFeatures", addContent);
                }

                var toDelete = currentFeatureIds.Except(selectedFeatureIds).ToList();

                foreach (var featureId in toDelete)
                {
                    var carFeatureToDelete = carFeaturesValues.FirstOrDefault(x => x.FeatureId == featureId);
                    if (carFeatureToDelete != null)
                    {
                        await client.DeleteAsync("https://localhost:7252/api/CarFeatures?id=" + carFeatureToDelete.CarFeatureId);
                    }
                }

                return RedirectToAction("Index", "Car");
            }

            return View("CreateCarModal", dto);
        }
    }
}
