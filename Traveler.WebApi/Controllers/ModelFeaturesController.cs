using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.ModelFeatureDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelFeaturesController : ControllerBase
    {
        private readonly IModelFeatureDal _modelFeatureDal;

        public ModelFeaturesController(IModelFeatureDal modelFeatureDal)
        {
            _modelFeatureDal = modelFeatureDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModelFeatures()
        {
            var values = await _modelFeatureDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelFeatureById(int id)
        {
            var value = await _modelFeatureDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetModelFeaturesByModelId/{modelId}")]
        public async Task<IActionResult> GetModelFeaturesByModelId(int modelId)
        {
            var values = await _modelFeatureDal.GetModelFeaturesByModelId(modelId);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModelFeature(CreateModelFeatureDto model)
        {
            var modelFeature = new ModelFeature
            {
                Available = model.Available,
                ModelId = model.ModelId,
                FeatureId = model.FeatureId,
            };

            await _modelFeatureDal.CreateAsync(modelFeature);

            return Ok("ModelFeature information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateModelFeature(UpdateModelFeatureDto model)
        {
            var modelFeature = new ModelFeature
            {
                ModelFeatureId = model.ModelFeatureId,
                Available = model.Available,
                ModelId = model.ModelId,
                FeatureId = model.FeatureId,
            };
            await _modelFeatureDal.UpdateAsync(modelFeature);

            return Ok("ModelFeature information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveModelFeature(int id)
        {
            var value = await _modelFeatureDal.GetByIdAsync(id);

            await _modelFeatureDal.RemoveAsync(value);

            return Ok("ModelFeature information has been removed.");
        }
    }
}
