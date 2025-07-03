using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.ManagementDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementsController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IManagementDal _managementDal;

        public ManagementsController(SignInManager<AppUser> signInManager, IManagementDal managementDal)
        {
            _signInManager = signInManager;
            _managementDal = managementDal;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _managementDal.GetAllUsers();
            if (users.Any())
            {
                return Ok(users);
            }

            return BadRequest("Hiçbir kullanıcı bulunamadı.");
        }

        [HttpGet("FindByName/{userName}")]
        public async Task<IActionResult> FindByName(string userName)
        {
            var user = await _managementDal.FindByNameAsync(userName);

            return Ok(user);
        }

        [HttpGet("CheckEmailConfirmed/{email}")]
        public async Task<IActionResult> CheckEmailConfirmed(string email)
        {
            try
            {
                var check = await _managementDal.CheckEmailConfirmed(email);
                return Ok(check);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Beklenmeyen bir hata oluştu.", details = ex.Message });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            if (ModelState.IsValid)
            {
                var existingUserByEmail = await _managementDal.FindByEmailAsync(request.Email);

                if (existingUserByEmail != null)
                {
                    return BadRequest(new { code = "10", message = "Girilen Email başka hesap tarafından kullanılmaktadır." });
                }

                (bool, AppUser) result = await _managementDal.Register(request);

                if (result.Item1)
                {
                    return Ok("Kayıt işlemi başarılı.");
                }

                return BadRequest(new { code = "99", message = "Kayıt işlemi başarısız, lütfen daha sonra tekrar deneyiniz." });
            }
            return BadRequest(new { code = "98", message = "Geçersiz model verisi gönderildi." });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            if (!string.IsNullOrEmpty(request.Email))
            {
                var model = await _managementDal.Login(request);
                if (model.Item1)
                {
                    await _signInManager.SignInAsync(model.Item2, isPersistent: false);
                    return Ok(new
                    {
                        message = "Giriş başarılı.",
                        user = new
                        {
                            id = model.Item2.Id,
                            email = model.Item2.Email,
                            userName = model.Item2.UserName,
                            name = model.Item2.Name,
                            surname = model.Item2.Surname
                        }
                    });
                }
                else
                {
                    return BadRequest("Giriş işlemi başarısız.");
                }
            }
            return Unauthorized("Email bulunamadı.");
        }

        [HttpPost("CheckVerificationCode")]
        public async Task<IActionResult> CheckVerificationCode(string email, string verificationCode)
        {
            try
            {
                await _managementDal.CheckVerificationCodeAsync(email, verificationCode);

                return Ok(new { message = "Doğrulama başarılı." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Beklenmeyen bir hata oluştu.", details = ex.Message });
            }
        }

        [HttpPost("ChangeVerificationCode")]
        public async Task<IActionResult> ChangeVerificationCode(string email)
        {
            try
            {
                await _managementDal.ChangeVerificationCode(email);

                return Ok(new { message = "Doğrulama kodu değiştirildi." });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Beklenmeyen bir hata oluştu.", details = ex.Message });
            }
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geçersiz istek");
            }

            var result = await _managementDal.SendPasswordResetEmailAsync(email);

            if (result)
            {
                return Ok("Şifre sıfırlama e-postası başarıyla gönderildi.");
            }

            return BadRequest("Şifre sıfırlama işlemi başarısız oldu.");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.NewPassword) || string.IsNullOrEmpty(model.NewPasswordCheck))
            {
                return BadRequest("Geçersiz şifre.");
            }

            if (model.NewPassword != model.NewPasswordCheck)
            {
                return BadRequest("Şifreler eşleşmiyor.");
            }

            var user = await _managementDal.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var result = await _managementDal.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Şifre başarıyla sıfırlandı.");
            }

            return BadRequest("Şifre sıfırlama işlemi başarısız oldu.");
        }
    }
}
