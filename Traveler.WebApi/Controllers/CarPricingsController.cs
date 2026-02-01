using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.ModelPricingDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelPricingsController : ControllerBase
    {
        private readonly IModelPricingDal _modelPricingDal;

        public ModelPricingsController(IModelPricingDal modelPricingDal)
        {
            _modelPricingDal = modelPricingDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModelPricings()
        {
            var values = await _modelPricingDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelPricingById(int id)
        {
            var value = await _modelPricingDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetModelPricingsByModelId/{modelId}")]
        public async Task<IActionResult> GetModelPricingsByModelId(int modelId)
        {
            var values = await _modelPricingDal.GetModelPricingsByModelId(modelId);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModelPricing(CreateModelPricingDto model)
        {
            var modelPricing = new ModelPricing()
            {
                ModelId = model.ModelId,
                Amount = model.Amount,
                PricingId = model.PricingId,
            };

            await _modelPricingDal.CreateAsync(modelPricing);

            return Ok("ModelPricing information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateModelPricing(UpdateModelPricingDto model)
        {
            var modelPricing = new ModelPricing
            {
                ModelPricingId = model.ModelPricingId,
                ModelId = model.ModelId,
                Amount = model.Amount,
                PricingId = model.PricingId,
            };
            await _modelPricingDal.UpdateAsync(modelPricing);

            return Ok("ModelPricing information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveModelPricing(int id)
        {
            var value = await _modelPricingDal.GetByIdAsync(id);

            await _modelPricingDal.RemoveAsync(value);

            return Ok("ModelPricing information has been removed.");
        }
    }
}
