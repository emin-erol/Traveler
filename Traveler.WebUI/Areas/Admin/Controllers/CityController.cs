using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.CityViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/City")]
    public class CityController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CityController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Cities");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCityViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [Route("CreateCityModal")]
        public IActionResult CreateCityModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateCity")]
        public async Task<IActionResult> CreateCity(CreateCityViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();
            
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/Cities", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "City");
            }

            return View();
        }

        [HttpGet]
        [Route("UpdateCityModal/{cityId}")]
        public async Task<IActionResult> UpdateCityModal(int cityId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Cities/" + cityId);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCityViewModel>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateCity")]
        public async Task<IActionResult> UpdateCity(UpdateCityViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Cities", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "City");
            }

            return View("UpdateCityModal", dto);
        }
    }
}
