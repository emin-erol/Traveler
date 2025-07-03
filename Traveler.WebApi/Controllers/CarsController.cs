using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.CarDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarDal _carDal;

        public CarsController(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var values = await _carDal.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllCars(int id)
        {
            var value = await _carDal.GetByIdAsync(id);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto model)
        {
            var car = new Car
            {
                BigImageUrl = model.BigImageUrl,
                BrandId = model.BrandId,
                CarClassId = model.CarClassId,
                CoverImageUrl = model.CoverImageUrl,
                Description = model.Description,
                Fuel = model.Fuel,
                Luggage = model.Luggage,
                Mileage = model.Mileage,
                Model = model.Model,
                Seat = model.Seat,
                Transmission = model.Transmission,
                Year = model.Year,
            };

            await _carDal.CreateAsync(car);

            return Ok("Car information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar(UpdateCarDto model)
        {
            var car = new Car
            {
                CarId = model.CarId,
                BigImageUrl = model.BigImageUrl,
                BrandId = model.BrandId,
                CarClassId = model.CarClassId,
                CoverImageUrl = model.CoverImageUrl,
                Description = model.Description,
                Fuel = model.Fuel,
                Luggage = model.Luggage,
                Mileage = model.Mileage,
                Model = model.Model,
                Seat = model.Seat,
                Transmission = model.Transmission,
                Year = model.Year,
            };

            await _carDal.UpdateAsync(car);

            return Ok("Car information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCar(int id)
        {
            var value = await _carDal.GetByIdAsync(id);

            await _carDal.RemoveAsync(value);

            return Ok("Car information has been removed.");
        }
    }
}
