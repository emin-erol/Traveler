using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Traveler.Application.Dtos.ManagementDtos;

namespace Traveler.Persistence.Repositories
{
    public class ManagementRepository : IManagementDal
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TravelerDbContext _context;
        private readonly IConfiguration _configuration;

        private readonly SmtpClient _smtpClient;
        private readonly string _fromAddress = "noreply@personalwebsite.com";
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "erolemin76@gmail.com";
        private readonly string _smtpPass = "oemwerljjnnehchv";

        public ManagementRepository(UserManager<AppUser> userManager, TravelerDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;

            _smtpClient = new SmtpClient(_smtpHost)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<(bool, AppUser)> Login(LoginDto model)
        {
            var user = await this.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var check = await this.CheckPasswordAsync(user, model.Password);
                if (check)
                {
                    return (true, user);
                }
                else
                {
                    return (false, user);
                }
            }
            return (false, null);
        }

        public async Task<(bool, AppUser)> Register(RegisterDto model)
        {
            var verificationCode = GenerateVerificationCode();
            var randomUserNumber = GenerateVerificationCode();
            var userName = model.Name + model.Surname + randomUserNumber;
            var userModel = new AppUser { 
                Name = model.Name,
                Surname = model.Surname,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                DriverLicenseDate = model.DriverLicenseDate,
                PhoneNumber = model.PhoneNumber,
                UserName = userName,
                Email = model.Email,
                VerificationCode = verificationCode,
                Address = model.Address,
                AccessFailedCount = 0,
            };
            var result = await _userManager.CreateAsync(userModel, model.Password);

            if (result.Succeeded)
            {
                await SendVerificationMail(userModel.Email, verificationCode);
                return (true, userModel);
            }
            else
            {
                return (false, userModel);
            }
        }

        public string GenerateVerificationCode()
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            return code;
        }

        public async Task CheckVerificationCodeAsync(string email, string verificationCode)
        {
            var user = await this.FindByEmailAsync(email);

            if (user != null)
            {
                if (user.VerificationCode == verificationCode)
                {
                    user.EmailConfirmed = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException("Doğrulama kodu hatalı.");
                }
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı bulunamadı.");
            }

        }

        public async Task SendVerificationMail(string email, string verificationCode)
        {
            var subject = "Doğrulama Kodu";
            var body = $@"
            <html>
                <body>
                    <h3>Merhaba,</h3>
                    <p>Kayıt işleminiz başarıyla tamamlandı. İşte doğrulama kodunuz:</p>
                    <h2>{verificationCode}</h2>
                    <p>Bu kodu kullanarak hesabınızı doğrulayabilirsiniz.</p>
                    <br/>
                    <p>Teşekkür ederiz!</p>
                </body>
            </html>";

            var message = new MailMessage
            {
                From = new MailAddress(_fromAddress, "noreply@personalwebsite.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            message.To.Add(email);

            await _smtpClient.SendMailAsync(message);
        }

        public async Task<bool> SendPasswordResetEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = $"{_configuration["AppSettings:FrontendBaseUrl"]}/Management/ResetPasswordModal?email={WebUtility.UrlEncode(email)}&token={WebUtility.UrlEncode(resetToken)}";

            var mailBody = $@"
            <p>Merhaba {user.UserName},</p>
            <p>Şifre sıfırlama talebinde bulundunuz. Şifrenizi sıfırlamak için aşağıdaki bağlantıya tıklayabilirsiniz:</p>
            <p><a href='{resetUrl}'>Şifre Sıfırla</a></p>
            <p>Bu bağlantı yalnızca bir kez kullanılabilir ve güvenlik amacıyla oluşturulmuştur. Eğer bu işlemi siz başlatmadıysanız, lütfen bu e-postayı dikkate almayınız.</p>
            <p>Teşekkürler,</p>
            <p>Destek Ekibi</p>";

            var smtpHost = _configuration["EmailSettings:Host"];
            var smtpPort = int.Parse(_configuration["EmailSettings:Port"]!);
            var smtpUser = _configuration["EmailSettings:Username"];
            var smtpPass = _configuration["EmailSettings:Password"];

            try
            {
                using var smtpClient = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUser, "Destek Ekibi"),
                    Subject = "Şifre Sıfırlama Talebi",
                    Body = mailBody,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> CheckEmailConfirmed(string email)
        {
            var user = await this.FindByEmailAsync(email);

            if (user != null)
            {
                if (user.EmailConfirmed == true) return true;
                else return false;
            }

            else return false;
        }

        public async Task ChangeVerificationCode(string email)
        {
            var user = await this.FindByEmailAsync(email);
            if (user != null)
            {
                var verificationCode = GenerateVerificationCode();
                user.VerificationCode = verificationCode;

                await _context.SaveChangesAsync();

                await SendVerificationMail(email, verificationCode);
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı bulunamadı.");
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Token veya yeni şifre boş olamaz.");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
            }

            return result;
        }
    }
}
