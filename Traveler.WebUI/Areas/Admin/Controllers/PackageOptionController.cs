using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.PackageOptionViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/PackageOption")]
    public class PackageOptionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PackageOptionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/PackageOptions");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPackageOptionViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [Route("CreatePackageOptionModal")]
        public IActionResult CreatePackageOptionModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreatePackageOption")]
        public async Task<IActionResult> CreatePackageOption(CreatePackageOptionViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/PackageOptions", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "PackageOption");
            }

            return View("CreatePackageOptionModal", dto);
        }

        [HttpGet]
        [Route("UpdatePackageOptionModal/{packageOptionId}")]
        public async Task<IActionResult> UpdatePackageOptionModal(int packageOptionId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/PackageOptions/" + packageOptionId);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdatePackageOptionViewModel>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdatePackageOption")]
        public async Task<IActionResult> UpdatePackageOption(UpdatePackageOptionViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/PackageOptions", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "PackageOption");
            }

            return View("UpdatePackageOptionModal", dto);
        }
    }
}
