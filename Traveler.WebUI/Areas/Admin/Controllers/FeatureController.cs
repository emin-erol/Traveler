using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.FeatureViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Feature")]
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Features");

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [Route("CreateFeatureModal")]
        public IActionResult CreateFeatureModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateFeatureModal")]
        public async Task<IActionResult> CreateFeature(CreateFeatureViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/Features/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature");
            }

            return View("CreateCarModal", dto);
        }

        [HttpGet]
        [Route("UpdateFeatureModal/{featureId}")]
        public async Task<IActionResult> UpdateFeatureModal(int featureId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Features/" + featureId);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateFeatureViewModel>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Features/", content);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature");
            }

            return View("UpdateFeatureModal", dto);
        }
    }
}
