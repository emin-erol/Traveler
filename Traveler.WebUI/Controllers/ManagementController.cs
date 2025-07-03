using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using Traveler.ViewModel.ManagementViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Traveler.WebUI.Controllers
{
    public class ManagementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterModal()
        {
            var genderList = Enum.GetValues(typeof(GenderEnum))
                                 .Cast<GenderEnum>()
                                 .Select(g => new SelectListItem
                                 {
                                     Text = g.ToString(),
                                     Value = ((int)g).ToString()
                                 }).ToList();

            ViewBag.Genders = genderList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7252/api/Managements/Register", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var message = "Kayıt İşlemi Başarılı!";
                return RedirectToAction("VerifyModal", new { email = request.Email, message = message });
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return BadRequest(new { message = errorResponse });
        }

        [HttpGet]
        public IActionResult LoginModal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7252/api/Managements/Login", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var loginResult = JsonConvert.DeserializeObject<UserLoginResponseViewModel>(jsonResponse);
                var user = loginResult?.User;

                if (user == null)
                    return BadRequest("Kullanıcı bilgisi alınamadı.");

                var responseConfirm = await client.GetAsync("https://localhost:7252/api/Managements/CheckEmailConfirmed/" + request.Email);
                if (responseConfirm.IsSuccessStatusCode)
                {
                    var jsonConfirm = await responseConfirm.Content.ReadAsStringAsync();
                    var check = JsonConvert.DeserializeObject<bool>(jsonConfirm);

                    if (check)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email)
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return Json(new { success = true, redirectUrl = Url.Action("Index", "MainPage", new { area = "Admin" }) });
                    }
                    else
                    {
                        var message = "Email Doğrulama Yapınız!";
                        return Json(new { redirectUrl = Url.Action("VerifyModal", new { email = request.Email, message = message }) });
                    }
                }
            }

            return BadRequest("Giriş işlemi başarısız.");
        }

        [HttpGet]
        public IActionResult VerifyModal(string email, string message)
        {
            ViewBag.Email = email;
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Verify([FromBody] VerifyViewModel request)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var requestUrl = $"https://localhost:7252/api/Managements/CheckVerificationCode?email={request.Email}&verificationCode={request.VerificationCode}";

                var response = await client.PostAsync(requestUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, redirectUrl = Url.Action("LoginModal") });
                }
            }

            return BadRequest("Doğrulama kodu hatalı");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginModal", "Management");
        }

        [HttpGet]
        public IActionResult ForgetPasswordModal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] string email)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync("https://localhost:7252/api/Managements/ForgetPassword?email=" + email, null);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Şifre sıfırlama maili başarıyla gönderildi.");
            }

            return BadRequest("Şifre sıfırlama maili gönderilirken hata oluştu.");
        }

        public IActionResult ResetPasswordModal(string email, string token)
        {
            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync("https://localhost:7252/api/Managements/ResetPassword", model);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Şifre başarıyla sıfırlandı.");
            }

            return BadRequest("Şifre sıfırlanırken hata oluştu.");
        }

        public string GenerateVerificationCode()
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            return code;
        }

        public async Task<IActionResult> ChangeVerificationCode([FromBody] string email)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.PostAsync("https://localhost:7252/api/Managements/ChangeVerificationCode?email=" + email, null);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { success = true, message = "Doğrulama kodu yeniden gönderildi." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Doğrulama kodu gönderilemedi." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Bir hata oluştu: {ex.Message}" });
            }
        }
    }
}
