using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.BrandViewModels;
using Traveler.ViewModel.CarClassViewModels;
using Traveler.ViewModel.FeatureViewModels;
using Traveler.ViewModel.ModelFeatureViewModels;
using Traveler.ViewModel.ModelPricingViewModels;
using Traveler.ViewModel.ModelViewModels;
using Traveler.ViewModel.PricingViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Model")]
    public class ModelController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ModelController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("ModelDetail/{modelId}")]
        public async Task<IActionResult> ModelDetail(int modelId)
        {
            var client = _httpClientFactory.CreateClient();

            var brandResponse = await client.GetAsync("https://localhost:7252/api/Brands/GetBrandByModelId/" + modelId);
            var modelResponse = await client.GetAsync("https://localhost:7252/api/Models/GetModelWithAllDetails/" + modelId);
            var featureResponse = await client.GetAsync("https://localhost:7252/api/Features");
            var pricingResponse = await client.GetAsync("https://localhost:7252/api/Pricings");

            if (brandResponse.IsSuccessStatusCode &&
                modelResponse.IsSuccessStatusCode &&
                featureResponse.IsSuccessStatusCode &&
                pricingResponse.IsSuccessStatusCode)
            {
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var modelJson = await modelResponse.Content.ReadAsStringAsync();
                var featureJson = await featureResponse.Content.ReadAsStringAsync();
                var pricingJson = await pricingResponse.Content.ReadAsStringAsync();

                var brandValue = JsonConvert.DeserializeObject<ResultBrandViewModel>(brandJson);
                var modelValue = JsonConvert.DeserializeObject<GetModelWithAllDetailsViewModel>(modelJson);
                var featureValue = JsonConvert.DeserializeObject<List<ResultFeatureViewModel>>(featureJson);
                var pricingValue = JsonConvert.DeserializeObject<List<ResultPricingViewModel>>(pricingJson);

                ViewBag.Brand = brandValue;
                ViewBag.Features = featureValue;
                ViewBag.Pricings = pricingValue;

                return View(modelValue);
            }

            return View();
        }

        [Route("CreateModelModal/{brandId}")]
        public async Task<IActionResult> CreateModelModal(int brandId)
        {
            var client = _httpClientFactory.CreateClient();

            var brandResponse = await client.GetAsync("https://localhost:7252/api/Brands/" + brandId);
            var carClassesResponse = await client.GetAsync("https://localhost:7252/api/CarClasses");
            var pricingsResponse = await client.GetAsync("https://localhost:7252/api/Pricings");
            var featuresResponse = await client.GetAsync("https://localhost:7252/api/Features");

            if (brandResponse.IsSuccessStatusCode &&
                carClassesResponse.IsSuccessStatusCode &&
                pricingsResponse.IsSuccessStatusCode &&
                featuresResponse.IsSuccessStatusCode)
            {
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassesJson = await carClassesResponse.Content.ReadAsStringAsync();
                var pricingsJson = await pricingsResponse.Content.ReadAsStringAsync();
                var featuresJson = await featuresResponse.Content.ReadAsStringAsync();

                var brand = JsonConvert.DeserializeObject<ResultBrandViewModel>(brandJson);
                var carClasses = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassesJson);
                var pricings = JsonConvert.DeserializeObject<List<ResultPricingViewModel>>(pricingsJson);
                var features = JsonConvert.DeserializeObject<List<ResultFeatureViewModel>>(featuresJson);

                ViewBag.Brand = brand;
                ViewBag.CarClasses = carClasses;
                ViewBag.Pricings = pricings;
                ViewBag.Features = features;

                return View();
            }

            return View();
        }

        [Route("CreateModel")]
        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] CreateModelViewModel viewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var modelDto = new
            {
                ModelName = viewModel.ModelName,
                ModelDescription = viewModel.ModelDescription,
                CoverImageUrl = viewModel.CoverImageUrl,
                BigImageUrl = viewModel.BigImageUrl,
                Seat = viewModel.Seat,
                Luggage = viewModel.Luggage,
                BrandId = viewModel.BrandId,
                CarClassId = viewModel.CarClassId,
            };

            var modelJson = JsonConvert.SerializeObject(modelDto);

            StringContent content = new StringContent(modelJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/Models", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var createdModel = JsonConvert.DeserializeObject<ResultModelViewModel>(responseData)!;

                int modelId = createdModel.ModelId;

                var modelFeatures = viewModel.ModelFeatures;

                if(modelFeatures != null)
                {
                    foreach( var modelFeature in modelFeatures)
                    {
                        modelFeature.ModelId = modelId;

                        var mfJson = JsonConvert.SerializeObject(modelFeature);
                        StringContent mfContent = new StringContent(mfJson, Encoding.UTF8, "application/json");
                        await client.PostAsync("https://localhost:7252/api/ModelFeatures", mfContent);
                    }
                }

                var modelPricings = viewModel.ModelPricings;

                if(modelPricings != null)
                {
                    foreach(var modelPricing in modelPricings)
                    {
                        modelPricing.ModelId = modelId;

                        var mpJson = JsonConvert.SerializeObject(modelPricing);
                        StringContent mpContent = new StringContent(mpJson, Encoding.UTF8, "application/json");
                        await client.PostAsync("https://localhost:7252/api/ModelPricings", mpContent);
                    }
                }

                return Json(new { success = true, message = "Model başarıyla oluşturuldu." });
            }


            return Json(new { success = false, message = "Model oluşturulurken bir hata oluştu." });
        }

        [HttpGet("UpdateModelModal/{modelId}")]
        [Route("UpdateModelModal")]
        public async Task<IActionResult> UpdateModelModal(int modelId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Models/" + modelId);
            var brandResponse = await client.GetAsync("https://localhost:7252/api/Brands");
            var carClassResponse = await client.GetAsync("https://localhost:7252/api/CarClasses");

            if (response.IsSuccessStatusCode &&
                brandResponse.IsSuccessStatusCode &&
                carClassResponse.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var brandJson = await brandResponse.Content.ReadAsStringAsync();
                var carClassJson = await carClassResponse.Content.ReadAsStringAsync();

                var value = JsonConvert.DeserializeObject<UpdateModelViewModel>(jsonData);
                var brandValues = JsonConvert.DeserializeObject<List<ResultBrandViewModel>>(brandJson);
                var carClassValues = JsonConvert.DeserializeObject<List<ResultCarClassViewModel>>(carClassJson);

                ViewBag.Brands = brandValues;
                ViewBag.CarClass = carClassValues;

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateModel")]
        public async Task<IActionResult> UpdateModel(UpdateModelViewModel viewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(viewModel);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Models", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Brand");
            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("UpdateModelFeatures")]
        public async Task<IActionResult> UpdateModelFeatures([FromBody] List<ResultModelFeatureViewModel> features)
        {
            var client = _httpClientFactory.CreateClient();

            if (features == null || !features.Any())
                return BadRequest("Özellik listesi boş olamaz.");

            bool allSuccessful = true;

            foreach (var feature in features)
            {
                var jsonData = JsonConvert.SerializeObject(feature);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:7252/api/ModelFeatures", content);

                if (!response.IsSuccessStatusCode)
                    allSuccessful = false;
            }

            if (allSuccessful)
            {
                return Ok(new { message = "Tüm özellikler başarıyla güncellendi." });
            }
            else
            {
                return StatusCode(500, new { message = "Bazı özellikler güncellenemedi." });
            }
        }

        [HttpPost]
        [Route("UpdateModelPricings")]
        public async Task<IActionResult> UpdateModelPricings([FromBody] List<ResultModelPricingViewModel> pricings)
        {
            var client = _httpClientFactory.CreateClient();

            if (pricings == null || !pricings.Any())
                return BadRequest("Fiyat listesi boş olamaz.");

            bool allSuccessful = true;

            foreach (var pricing in pricings)
            {
                var jsonData = JsonConvert.SerializeObject(pricing);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:7252/api/ModelPricings", content);

                if (!response.IsSuccessStatusCode)
                    allSuccessful = false;
            }

            if (allSuccessful)
            {
                return Ok(new { message = "Tüm fiyatlar başarıyla güncellendi." });
            }
            else
            {
                return StatusCode(500, new { message = "Bazı fiyatlar güncellenemedi." });
            }
        }
    }
}
