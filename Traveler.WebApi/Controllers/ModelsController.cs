using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.ModelDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelDal _modelDal;

        public ModelsController(IModelDal modelDal)
        {
            _modelDal = modelDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            var values = await _modelDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelById(int id)
        {
            var value = await _modelDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetModelsByBrand/{brandId}")]
        public async Task<IActionResult> GetModelsByBrand(int brandId)
        {
            var values = await _modelDal.GetModelsByBrand(brandId);

            return Ok(values);
        }

        [HttpGet("GetBrandNameByModelName/{modelName}")]
        public async Task<IActionResult> GetBrandNameByModelName(string modelName)
        {
            var value = await _modelDal.GetBrandNameByModelName(modelName);

            return Ok(value);
        }

        [HttpGet("GetModelWithAllDetails/{modelId}")]
        public async Task<IActionResult> GetModelWithAllDetails(int modelId)
        {
            var value = await _modelDal.GetModelWithAllDetails(modelId);

            return Ok(value);
        }

        [HttpGet("GetAllModelsWithDetailsByLocation/{locationId}")]
        public async Task<IActionResult> GetAllModelsWithDetailsByLocation(int locationId)
        {
            var values = await _modelDal.GetAllModelsWithDetailsByLocation(locationId);

            return Ok(values);
        }

        [HttpGet("GetMostSuitableCarIdByModelId/{modelId}/{locationId}/{pickUpDate}/{dropOffDate}")]
        public async Task<IActionResult> GetMostSuitableCarIdByModelId(int modelId, int locationId, DateOnly pickUpDate, DateOnly dropOffDate)
        {
            var value = await _modelDal.GetMostSuitableCarIdByModelId(modelId, locationId, pickUpDate, dropOffDate);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(CreateModelDto model)
        {
            var modelDto = new Model
            {
                BigImageUrl = model.BigImageUrl,
                BrandId = model.BrandId,
                CarClassId = model.CarClassId,
                CoverImageUrl = model.CoverImageUrl,
                Luggage = model.Luggage,
                ModelDescription = model.ModelDescription,
                ModelName = model.ModelName,
                Seat = model.Seat,
            };

            await _modelDal.CreateAsync(modelDto);

            return Ok(modelDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateModel(UpdateModelDto model)
        {
            var modelDto = new Model
            {
                ModelId = model.ModelId,
                BigImageUrl = model.BigImageUrl,
                BrandId = model.BrandId,
                CarClassId = model.CarClassId,
                CoverImageUrl = model.CoverImageUrl,
                Luggage = model.Luggage,
                ModelDescription = model.ModelDescription,
                ModelName = model.ModelName,
                Seat = model.Seat,
            };
            await _modelDal.UpdateAsync(modelDto);

            return Ok("Model information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveModel(int id)
        {
            var value = await _modelDal.GetByIdAsync(id);

            await _modelDal.RemoveAsync(value);

            return Ok("Model information has been removed.");
        }
    }
}
