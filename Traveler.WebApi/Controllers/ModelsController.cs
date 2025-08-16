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

            return Ok("Model information has been created.");
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
