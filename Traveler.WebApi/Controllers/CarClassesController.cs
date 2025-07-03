using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.CarClassDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarClassesController : ControllerBase
    {
        private readonly ICarClassDal _carClassDal;

        public CarClassesController(ICarClassDal carClassDal)
        {
            _carClassDal = carClassDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarClasss()
        {
            var values = await _carClassDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarClassById(int id)
        {
            var value = await _carClassDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarClass(CreateCarClassDto model)
        {
            var carClass = new CarClass
            {
                ClassName = model.ClassName,
            };

            await _carClassDal.CreateAsync(carClass);

            return Ok("CarClass information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarClass(UpdateCarClassDto model)
        {
            var carClass = new CarClass
            {
                CarClassId = model.CarClassId,
                ClassName = model.ClassName,
            };
            await _carClassDal.UpdateAsync(carClass);

            return Ok("CarClass information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCarClass(int id)
        {
            var value = await _carClassDal.GetByIdAsync(id);

            await _carClassDal.RemoveAsync(value);

            return Ok("CarClass information has been removed.");
        }
    }
}
