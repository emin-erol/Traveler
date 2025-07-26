using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using Traveler.ViewModel.MileagePackageViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/MileagePackage")]
    public class MileagePackageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MileagePackageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/MileagePackages");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMileagePackageViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [Route("CreateMileagePackageModal")]
        public IActionResult CreateMileageModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateMileagePackage")]
        public async Task<IActionResult> CreateMileagePackage(CreateMileagePackageViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/MileagePackages", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "MileagePackage");
            }

            return View("CreateMileagePackageModal", dto);
        }

        [HttpGet]
        [Route("UpdateMileagePackageModal/{mileagePackageId}")]
        public async Task<IActionResult> UpdateMileagePackageModal(int mileagePackageId)
        {
            var client = _httpClientFactory.CreateClient();

            var resposne = await client.GetAsync("https://localhost:7252/api/MileagePackages/" + mileagePackageId);

            if(resposne.IsSuccessStatusCode)
            {
                var jsonData = await resposne.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateMileagePackageViewModel>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateMileagePackage")]
        public async Task<IActionResult> UpdateMileagePackage(UpdateMileagePackageViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/MileagePackages", content);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "MileagePackage");
            }

            return View("UpdateMileagePackageModal", dto);
        }
    }
}
