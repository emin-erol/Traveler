using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Traveler.Application.Dtos.FeatureDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureDal _featureDal;

        public FeaturesController(IFeatureDal featureDal)
        {
            _featureDal = featureDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeatures()
        {
            var values = await _featureDal.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById(int id)
        {
            var value = await _featureDal.GetByIdAsync(id);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto model)
        {
            var feature = new Feature
            {
                FeatureName = model.FeatureName,
            };

            await _featureDal.CreateAsync(feature);

            return Ok("Feature information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto model)
        {
            var feature = new Feature
            {
                FeatureId = model.FeatureId,
                FeatureName = model.FeatureName,
            };

            await _featureDal.UpdateAsync(feature);

            return Ok("Feature information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFeature(int id)
        {
            var value = await _featureDal.GetByIdAsync(id);

            await _featureDal.RemoveAsync(value);

            return Ok("Feature information has been removed.");
        }
    }
}
