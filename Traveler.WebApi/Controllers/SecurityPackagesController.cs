using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.SecurityPackageDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityPackagesController : ControllerBase
    {
        private readonly ISecurityPackageDal _securityPackageDal;

        public SecurityPackagesController(ISecurityPackageDal securityPackageDal)
        {
            _securityPackageDal = securityPackageDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSecurityPackages()
        {
            var values = await _securityPackageDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecurityPackageById(int id)
        {
            var value = await _securityPackageDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetAllSecurityPackagesWithPackageOptions")]
        public async Task<IActionResult> GetAllSecurityPackagesWithPackageOptions()
        {
            var values = await _securityPackageDal.GetAllSecurityPackagesWithPackageOptions();

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecurityPackage(CreateSecurityPackageDto model)
        {
            var securityPackage = new SecurityPackage
            {
                Amount = model.Amount,
                PackageName = model.PackageName,
            };

            await _securityPackageDal.CreateAsync(securityPackage);

            return Ok(securityPackage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSecurityPackage(UpdateSecurityPackageDto model)
        {
            var securityPackage = new SecurityPackage
            {
                SecurityPackageId = model.SecurityPackageId,
                PackageName = model.PackageName,
                Amount = model.Amount,
            };
            await _securityPackageDal.UpdateAsync(securityPackage);

            return Ok("SecurityPackage information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSecurityPackage(int id)
        {
            var value = await _securityPackageDal.GetByIdAsync(id);

            await _securityPackageDal.RemoveAsync(value);

            return Ok("SecurityPackage information has been removed.");
        }
    }
}
