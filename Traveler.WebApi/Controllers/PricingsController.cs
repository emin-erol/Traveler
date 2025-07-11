using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.PricingDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingsController : ControllerBase
    {
        private readonly IPricingDal _pricingDal;

        public PricingsController(IPricingDal pricingDal)
        {
            _pricingDal = pricingDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPricings()
        {
            var values = await _pricingDal.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPricingById(int id)
        {
            var value = await _pricingDal.GetByIdAsync(id);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePricing(CreatePricingDto model)
        {
            var pricing = new Pricing
            {
                PricingType = model.PricingType,
                PricingDec = model.PricingDec,
            };

            await _pricingDal.CreateAsync(pricing);

            return Ok("Pricing information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePricing(UpdatePricingDto model)
        {
            var pricing = new Pricing
            {
                PricingId = model.PricingId,
                PricingType = model.PricingType,
                PricingDec = model.PricingDec,
            };

            await _pricingDal.UpdateAsync(pricing);

            return Ok("Pricing information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePricing(int id)
        {
            var value = await _pricingDal.GetByIdAsync(id);

            await _pricingDal.RemoveAsync(value);

            return Ok("Pricing information has been removed.");
        }
    }
}
