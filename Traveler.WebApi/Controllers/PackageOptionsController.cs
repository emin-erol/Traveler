using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.PackageOptionDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageOptionsController : ControllerBase
    {
        private readonly IPackageOptionDal _packageOptionDal;

        public PackageOptionsController(IPackageOptionDal packageOptionDal)
        {
            _packageOptionDal = packageOptionDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPackageOptions()
        {
            var values = await _packageOptionDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageOptionById(int id)
        {
            var value = await _packageOptionDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackageOption(CreatePackageOptionDto model)
        {
            var packageOption = new PackageOption
            {
                OptionName = model.OptionName,
            };

            await _packageOptionDal.CreateAsync(packageOption);

            return Ok("PackageOption information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePackageOption(UpdatePackageOptionDto model)
        {
            var packageOption = new PackageOption
            {
                PackageOptionId = model.PackageOptionId,
                OptionName = model.OptionName,
            };
            await _packageOptionDal.UpdateAsync(packageOption);

            return Ok("PackageOption information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePackageOption(int id)
        {
            var value = await _packageOptionDal.GetByIdAsync(id);

            await _packageOptionDal.RemoveAsync(value);

            return Ok("PackageOption information has been removed.");
        }
    }
}
