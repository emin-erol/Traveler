using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.CarFeatureDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFeaturesController : ControllerBase
    {
        private readonly ICarFeatureDal _carFeatureDal;

        public CarFeaturesController(ICarFeatureDal carFeatureDal)
        {
            _carFeatureDal = carFeatureDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarFeatures()
        {
            var values = await _carFeatureDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarFeatureById(int id)
        {
            var value = await _carFeatureDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarFeature(CreateCarFeatureDto model)
        {
            var carFeature = new CarFeature
            {
                Available = model.Available,
                CarId = model.CarId,
                FeatureId = model.FeatureId,
            };

            await _carFeatureDal.CreateAsync(carFeature);

            return Ok("CarFeature information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarFeature(UpdateCarFeatureDto model)
        {
            var carFeature = new CarFeature
            {
                CarFeatureId = model.CarFeatureId,
                Available = model.Available,
                CarId = model.CarId,
                FeatureId = model.FeatureId,
            };
            await _carFeatureDal.UpdateAsync(carFeature);

            return Ok("CarFeature information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCarFeature(int id)
        {
            var value = await _carFeatureDal.GetByIdAsync(id);

            await _carFeatureDal.RemoveAsync(value);

            return Ok("CarFeature information has been removed.");
        }
    }
}
