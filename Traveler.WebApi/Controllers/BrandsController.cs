using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.BrandDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandDal _brandDal;

        public BrandsController(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var values = await _brandDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var value = await _brandDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetBrandByModelId/{modelId}")]
        public async Task<IActionResult> GetBrandByModelId(int modelId)
        {
            var value = await _brandDal.GetBrandByModelId(modelId);

            return Ok(value);
        }

        [HttpGet("GetBrandsWithModels")]
        public async Task<IActionResult> GetBrandsWithModels()
        {
            var values = await _brandDal.GetBrandsWithModels();

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto model)
        {
            var brand = new Brand
            {
                Name = model.Name,
            };

            await _brandDal.CreateAsync(brand);

            return Ok("Brand information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto model)
        {
            var brand = new Brand
            {
                BrandId = model.BrandId,
                Name = model.Name,
            };

            await _brandDal.UpdateAsync(brand);

            return Ok("Brand information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            var value = await _brandDal.GetByIdAsync(id);

            await _brandDal.RemoveAsync(value);

            return Ok("Brand information has been removed.");
        }
    }
}
