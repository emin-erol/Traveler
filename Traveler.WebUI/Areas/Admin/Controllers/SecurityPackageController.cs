using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traveler.ViewModel.SecurityPackageOptionViewModels;
using Traveler.ViewModel.PackageOptionViewModels;
using Traveler.ViewModel.SecurityPackageViewModels;

namespace Traveler.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SecurityPackage")]
    public class SecurityPackageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SecurityPackageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/SecurityPackages/GetAllSecurityPackagesWithPackageOptions");

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetSecurityPackagesWithPackageOptionsViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateSecurityPackageModal")]
        public async Task<IActionResult> CreateSecurityPackageModal()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7252/api/PackageOptions");

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var packageOptionsValues = JsonConvert.DeserializeObject<List<ResultPackageOptionViewModel>>(jsonData);

                ViewBag.PackageOptions = packageOptionsValues;
            }

            return View();
        }

        [HttpPost]
        [Route("CreateSecurityPackage")]
        public async Task<IActionResult> CreateSecurityPackage(CreateSecurityPackageViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7252/api/SecurityPackages", content);

            if(response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var createdSecurityPackage = JsonConvert.DeserializeObject<ResultSecurityPackageViewModel>(responseData)!;

                int securityPackageId = createdSecurityPackage.SecurityPackageId;

                if (dto.SelectedPackageOptionIds != null && dto.SelectedPackageOptionIds.Any())
                {
                    foreach (var packageOptionId in dto.SelectedPackageOptionIds)
                    {
                        var createSecurityPackageOptionViewModel = new
                        {
                            SecurityPackageId = securityPackageId,
                            PackageOptionId = packageOptionId
                        };

                        var securityPackageOptionJson = JsonConvert.SerializeObject(createSecurityPackageOptionViewModel);
                        var securityPackageOptionContent = new StringContent(securityPackageOptionJson, Encoding.UTF8, "application/json");

                        await client.PostAsync("https://localhost:7252/api/SecurityPackageOptions", securityPackageOptionContent);
                    }
                }

                return RedirectToAction("Index", "SecurityPackage");
            }

            return View("CreateSecurityPackageModal", dto);
        }

        [HttpGet]
        [Route("UpdateSecurityPackageModal/{securityPackageId}")]
        public async Task<IActionResult> UpdateSecurityPackageModal(int securityPackageId)
        {
            var client = _httpClientFactory.CreateClient();

            var securityPackageResponse = await client.GetAsync("https://localhost:7252/api/SecurityPackages/" + securityPackageId);
            var packageOptionsResponse = await client.GetAsync("https://localhost:7252/api/PackageOptions");
            var securityPackageOptionsResponse = await client.GetAsync("https://localhost:7252/api/SecurityPackageOptions/GetSecurityPackageOptionsBySecurityPackageId/" + securityPackageId);

            if(securityPackageResponse.IsSuccessStatusCode &&
                packageOptionsResponse.IsSuccessStatusCode &&
                securityPackageOptionsResponse.IsSuccessStatusCode)
            {
                var securityPackageJson = await securityPackageResponse.Content.ReadAsStringAsync();
                var packageOptionsJson = await packageOptionsResponse.Content.ReadAsStringAsync();
                var securityPackageOptionsJson = await securityPackageOptionsResponse.Content.ReadAsStringAsync();

                var securityPackageValue = JsonConvert.DeserializeObject<UpdateSecurityPackageViewModel>(securityPackageJson);
                var packageOptionsValues = JsonConvert.DeserializeObject<List<ResultPackageOptionViewModel>>(packageOptionsJson);
                var securityPackageOptionsValues = JsonConvert.DeserializeObject<List<ResultSecurityPackageOptionViewModel>>(securityPackageOptionsJson);
                
                ViewBag.PackageOptions = packageOptionsValues;
                ViewBag.SecurityPackageOptions = securityPackageOptionsValues;

                return View(securityPackageValue);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateSecurityPackage")]
        public async Task<IActionResult> UpdateSecurityPackage(UpdateSecurityPackageViewModel dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7252/api/SecurityPackages", content);

            if (response.IsSuccessStatusCode)
            {
                var securityPackageOptionsResponse = await client.GetAsync("https://localhost:7252/api/SecurityPackageOptions/GetSecurityPackageOptionsBySecurityPackageId/" + dto.SecurityPackageId);
                var securityPackageOptionsJson = await securityPackageOptionsResponse.Content.ReadAsStringAsync();
                var securityPackageOptionsValues = JsonConvert.DeserializeObject<List<ResultSecurityPackageOptionViewModel>>(securityPackageOptionsJson);

                var currentPackageOptionIds = securityPackageOptionsValues.Select(x => x.PackageOptionId).ToList();
                var selectedPackageOptionIds = dto.SelectedPackageOptionIds ?? new List<int>();

                var toAdd = selectedPackageOptionIds.Except(currentPackageOptionIds).ToList();

                foreach (var packageOptionId in toAdd)
                {
                    var addContent = new StringContent(
                        JsonConvert.SerializeObject(new
                        {
                            SecurityPackageId = dto.SecurityPackageId,
                            PackageOptionId = packageOptionId
                        }),
                        Encoding.UTF8,
                        "application/json");

                    await client.PostAsync("https://localhost:7252/api/SecurityPackageOptions", addContent);
                }

                var toDelete = currentPackageOptionIds.Except(selectedPackageOptionIds).ToList();

                foreach (var packageOptionId in toDelete)
                {
                    var securityPackageOptionToDelete = securityPackageOptionsValues.FirstOrDefault(x => x.PackageOptionId == packageOptionId);
                    if (securityPackageOptionToDelete != null)
                    {
                        await client.DeleteAsync("https://localhost:7252/api/SecurityPackageOptions?id=" + securityPackageOptionToDelete.SecurityPackageOptionId);
                    }
                }

                return RedirectToAction("Index", "SecurityPackage");
            }

            return View("UpdateSecurityPackageModal", dto);
        }
    }
}
