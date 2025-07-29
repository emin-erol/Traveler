using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.SecurityPackageOptionDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityPackageOptionsController : ControllerBase
    {
        private readonly ISecurityPackageOptionDal _securityPackageOptionDal;

        public SecurityPackageOptionsController(ISecurityPackageOptionDal securityPackageOptionDal)
        {
            _securityPackageOptionDal = securityPackageOptionDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSecurityPackageOptions()
        {
            var values = await _securityPackageOptionDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecurityPackageOptionById(int id)
        {
            var value = await _securityPackageOptionDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetSecurityPackageOptionsBySecurityPackageId/{securityPackageId}")]
        public async Task<IActionResult> GetSecurityPackageOptionsBySecurityPackageId(int securityPackageId)
        {
            var values = await _securityPackageOptionDal.GetSecurityPackageOptionsBySecurityPackageId(securityPackageId);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecurityPackageOption(CreateSecurityPackageOptionDto model)
        {
            var securityPackageOption = new SecurityPackageOption
            {
                SecurityPackageId = model.SecurityPackageId,
                PackageOptionId = model.PackageOptionId
            };

            await _securityPackageOptionDal.CreateAsync(securityPackageOption);

            return Ok("SecurityPackageOption information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSecurityPackageOption(UpdateSecurityPackageOptionDto model)
        {
            var securityPackageOption = new SecurityPackageOption
            {
                SecurityPackageOptionId = model.SecurityPackageOptionId,
                SecurityPackageId = model.SecurityPackageId,
                PackageOptionId = model.PackageOptionId
            };
            await _securityPackageOptionDal.UpdateAsync(securityPackageOption);

            return Ok("SecurityPackageOption information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSecurityPackageOption(int id)
        {
            var value = await _securityPackageOptionDal.GetByIdAsync(id);

            await _securityPackageOptionDal.RemoveAsync(value);

            return Ok("SecurityPackageOption information has been removed.");
        }
    }
}
