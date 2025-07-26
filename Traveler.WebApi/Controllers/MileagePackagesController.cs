using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.MileageDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MileagePackagesController : ControllerBase
    {
        private readonly IMileagePackageDal _mileageDal;

        public MileagePackagesController(IMileagePackageDal mileageDal)
        {
            _mileageDal = mileageDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMileages()
        {
            var values = await _mileageDal.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMileageById(int id)
        {
            var value = await _mileageDal.GetByIdAsync(id);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMileage(CreateMileagePackageDto model)
        {
            var mileage = new MileagePackage
            {
                Amount = model.Amount,
                Description = model.Description,
                PackageLimit = model.PackageLimit,
                PackageName = model.PackageName,
            };

            await _mileageDal.CreateAsync(mileage);

            return Ok("Mileage info has been created!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMileage(UpdateMileagePackageDto model)
        {
            var mileage = new MileagePackage
            {
                MileagePackageId = model.MileagePackageId,
                Amount = model.Amount,
                Description = model.Description,
                PackageLimit = model.PackageLimit,
                PackageName = model.PackageName,
            };

            await _mileageDal.UpdateAsync(mileage);

            return Ok("Mileage info has been updated!");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveMileage(int id)
        {
            var mileage = await _mileageDal.GetByIdAsync(id);

            await _mileageDal.RemoveAsync(mileage);

            return Ok("Mileage info has been removed!");
        }
    }
}
