using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.CarPricingDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPricingsController : ControllerBase
    {
        private readonly ICarPricingDal _carPricingDal;

        public CarPricingsController(ICarPricingDal carPricingDal)
        {
            _carPricingDal = carPricingDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarPricings()
        {
            var values = await _carPricingDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarPricingById(int id)
        {
            var value = await _carPricingDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetCarPricingsByCarId/{carId}")]
        public async Task<IActionResult> GetCarPricingsByCarId(int carId)
        {
            var values = await _carPricingDal.GetCarPricingsByCarId(carId);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarPricing(CreateCarPricingDto model)
        {
            var carPricing = new CarPricing
            {
                CarId = model.CarId,
                Amount = model.Amount,
                PricingId = model.PricingId,
            };

            await _carPricingDal.CreateAsync(carPricing);

            return Ok("CarPricing information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarPricing(UpdateCarPricingDto model)
        {
            var carPricing = new CarPricing
            {
                CarPricingId = model.CarPricingId,
                CarId = model.CarId,
                Amount = model.Amount,
                PricingId = model.PricingId,
            };
            await _carPricingDal.UpdateAsync(carPricing);

            return Ok("CarPricing information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCarPricing(int id)
        {
            var value = await _carPricingDal.GetByIdAsync(id);

            await _carPricingDal.RemoveAsync(value);

            return Ok("CarPricing information has been removed.");
        }
    }
}
