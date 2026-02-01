using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.BrandViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class BrandController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BrandController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7252/api/Brands/GetBrandsWithModels");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetBrandsWithModelsViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [Route("CreateBrandModal")]
        public IActionResult CreateBrandModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateBrand")]
        public async Task<IActionResult> CreateBrand(CreateBrandViewModel viewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(viewModel);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/Brands", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Brand");
            }

            return View("CreateBrandModal", viewModel);
        }

        [HttpGet]
        [Route("UpdateBrandModal/{brandId}")]
        public async Task<IActionResult> UpdateBrandModal(int brandId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/Brands/" + brandId);

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateBrandViewModel>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandViewModel viewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(viewModel);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/Brands", content);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Brand");
            }

            return View("UpdateBrandModal", viewModel);
        }
    }
}
