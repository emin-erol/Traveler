using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.PricingViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Pricing")]
    public class PricingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PricingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7252/api/Pricings");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPricingViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [Route("CreatePricingModal")]
        public IActionResult CreatePricingModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreatePricingModal")]
        public async Task<IActionResult> CreatePricing(CreatePricingViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/Pricings/", content);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Pricing");
            }

            return View("CreatePricingModal", dto);
        }

        [HttpGet]
        [Route("UpdatePricingModal/{pricingId}")]
        public async Task<IActionResult> UpdatePricingModal(int pricingId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7252/api/Pricings/" + pricingId);

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdatePricingViewModel>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdatePricingModal")]
        public async Task<IActionResult> UpdatePricing(UpdatePricingViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Pricings/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Pricing");
            }

            return View("UpdatePricingModal", dto);
        }
    }
}
