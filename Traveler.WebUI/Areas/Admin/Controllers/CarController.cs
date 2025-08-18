using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.BrandViewModels;
using Traveler.ViewModel.CarClassViewModels;
using Traveler.ViewModel.CarFeatureViewModels;
using Traveler.ViewModel.CarPricingViewModels;
using Traveler.ViewModel.CarViewModels;
using Traveler.ViewModel.FeatureViewModels;
using Traveler.ViewModel.LocationViewModels;
using Traveler.ViewModel.ModelViewModels;
using Traveler.ViewModel.PricingViewModels;

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
            var pricingsResponse = await client.GetAsync("https://localhost:7252/api/Pricings");
            var locationsResponse = await client.GetAsync("https://localhost:7252/api/Locations");

            if (brandResponse.IsSuccessStatusCode &&
                carClassResponse.IsSuccessStatusCode &&
                featuresResponse.IsSuccessStatusCode &&
                pricingsResponse.IsSuccessStatusCode &&
                locationsResponse.IsSuccessStatusCode)
            {
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();
                var featuresJson = await featuresResponse.Content.ReadAsStringAsync();
                var pricingsJson = await pricingsResponse.Content.ReadAsStringAsync();
                var locationsJson = await locationsResponse.Content.ReadAsStringAsync();

                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);
                var featuresValues = JsonConvert.DeserializeObject<List<ResultFeatureViewModel>>(featuresJson);
                var pricingsValues = JsonConvert.DeserializeObject<List<ResultPricingViewModel>>(pricingsJson);
                var locationsValues = JsonConvert.DeserializeObject<List<ResultLocationViewModel>>(locationsJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClasses = carClassValues;
                ViewBag.Features = featuresValues;
                ViewBag.Pricings = pricingsValues;
                ViewBag.Locations = locationsValues;

                return View();
            }

            return View();
        }

        [HttpPost]
        [Route("CreateCar")]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarRequestViewModel request)
        {
            var dto = request.Dto;
            var cpDtos = request.CpDtos;

            dto.CreatedTime = DateTime.Now;
            dto.UpdatedTime = DateTime.Now;
            dto.LastUsedTime = DateTime.Now;

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
                        var createCarFeatureViewModel = new
                        {
                            CarId = newCarId,
                            FeatureId = featureId,
                            Available = true
                        };

                        var featureJson = JsonConvert.SerializeObject(createCarFeatureViewModel);
                        var featureContent = new StringContent(featureJson, Encoding.UTF8, "application/json");

                        await client.PostAsync("https://localhost:7252/api/CarFeatures", featureContent);
                    }
                }

                if (cpDtos != null)
                {
                    foreach(var cpDto in cpDtos)
                    {
                        var createCarPricingViewModel = new
                        {
                            CarId = newCarId,
                            PricingId = cpDto.PricingId,
                            Amount = cpDto.Amount,
                        };

                        var pricingJson = JsonConvert.SerializeObject(createCarPricingViewModel);
                        var pricingContent = new StringContent(pricingJson, Encoding.UTF8, "application/json");

                        await client.PostAsync("https://localhost:7252/api/CarPricings", pricingContent);
                    }
                }

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Car") });
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
            var pricingsResponse = await client.GetAsync("https://localhost:7252/api/Pricings");
            var carFeaturesResponse = await client.GetAsync("https://localhost:7252/api/CarFeatures/GetCarFeaturesByCarId/" + carId);
            var carPricingsResponse = await client.GetAsync("https://localhost:7252/api/CarPricings/GetCarPricingsByCarId/" + carId);
            var locationsResponse = await client.GetAsync("https://localhost:7252/api/Locations");

            if (carResponse.IsSuccessStatusCode &&
                brandResponse.IsSuccessStatusCode &&
                carClassResponse.IsSuccessStatusCode &&
                featuresResponse.IsSuccessStatusCode &&
                carFeaturesResponse.IsSuccessStatusCode &&
                carPricingsResponse.IsSuccessStatusCode &&
                pricingsResponse.IsSuccessStatusCode &&
                locationsResponse.IsSuccessStatusCode)
            {
                var carJson = await carResponse.Content.ReadAsStringAsync();
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();
                var featuresJson = await featuresResponse.Content.ReadAsStringAsync();
                var pricingsJson = await pricingsResponse.Content.ReadAsStringAsync();
                var carFeaturesJson = await carFeaturesResponse.Content.ReadAsStringAsync();
                var carPricingsJson = await carPricingsResponse.Content.ReadAsStringAsync();
                var locationsJson = await locationsResponse.Content.ReadAsStringAsync();

                var carValue = JsonConvert.DeserializeObject<UpdateCarViewModel>(carJson);
                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);
                var featuresValues = JsonConvert.DeserializeObject<List<ResultFeatureViewModel>>(featuresJson);
                var pricingsValues = JsonConvert.DeserializeObject<List<ResultPricingViewModel>>(pricingsJson);
                var carFeaturesValues = JsonConvert.DeserializeObject<List<ResultCarFeatureViewModel>>(carFeaturesJson);
                var carPricingsValues = JsonConvert.DeserializeObject<List<ResultCarPricingViewModel>>(carPricingsJson);
                var locationsValues = JsonConvert.DeserializeObject<List<ResultLocationViewModel>>(locationsJson);

                var selectedBrandResponse = await client.GetAsync("https://localhost:7252/api/Brands/GetBrandByModelId/" + carValue.ModelId);
                var selectedBrandJson = await selectedBrandResponse.Content.ReadAsStringAsync();
                var selectedBrandValue = JsonConvert.DeserializeObject<ResultBrandViewModel>(selectedBrandJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClasses = carClassValues;
                ViewBag.Features = featuresValues;
                ViewBag.Pricings = pricingsValues;
                ViewBag.CarFeatures = carFeaturesValues;
                ViewBag.CarPricings = carPricingsValues;
                ViewBag.Locations = locationsValues;
                ViewBag.SelectedBrand = selectedBrandValue;

                return View(carValue);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateCar")]
        public async Task<IActionResult> UpdateCar([FromBody] UpdateCarRequestViewModel request)
        {
            var dto = request.Dto;

            dto.UpdatedTime = DateTime.Now;

            var cpDtos = request.CpDtos;

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

                if (!cpDtos.IsNullOrEmpty())
                {
                    foreach( var cp in  cpDtos)
                    {
                        var cpJson = JsonConvert.SerializeObject(cp);
                        StringContent cpContent = new StringContent(cpJson, Encoding.UTF8, "application/json");

                        await client.PutAsync("https://localhost:7252/api/CarPricings/", cpContent);
                    }
                }

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Car") });
            }

            return View("CreateCarModal", dto);
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
