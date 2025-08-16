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

        [HttpGet("GetCarsWithBrandAndClass")]
        public async Task<IActionResult> GetCarsWithBrandAndClass()
        {
            var values = await _carDal.GetCarsWithBrandAndClass();

            return Ok(values);
        }

        [HttpGet("GetCarWithAllDetails/{carId}")]
        public async Task<IActionResult> GetCarWithAllDetails(int carId)
        {
            var value = await _carDal.GetCarWithAllDetails(carId);

            return Ok(value);
        }

        [HttpGet("GetCarsWithAllDetailsByLocation/{locationId}")]
        public async Task<IActionResult> GetCarsWithAllDetailsByLocation(int locationId)
        {
            var values = await _carDal.GetCarsWithAllDetailsByLocation(locationId);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto model)
        {
            var car = new Car
            {
                ModelId = model.ModelId,
                StockNumber = model.StockNumber,
                Description = model.Description,
                Fuel = model.Fuel,
                Mileage = model.Mileage,
                Transmission = model.Transmission,
                Year = model.Year,
                Status = model.Status,
                LocationId = model.LocationId,
                CreatedTime = model.CreatedTime,
                UpdatedTime = model.UpdatedTime,
                LastUsedTime = model.LastUsedTime,
                LicensePlate = model.LicensePlate,
            };

            await _carDal.CreateAsync(car);

            return Ok(car);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar(UpdateCarDto model)
        {
            var car = new Car
            {
                CarId = model.CarId,
                ModelId = model.ModelId,
                Description = model.Description,
                StockNumber = model.StockNumber,
                Fuel = model.Fuel,
                Mileage = model.Mileage,
                Transmission = model.Transmission,
                Year = model.Year,
                Status = model.Status,
                LocationId = model.LocationId,
                CreatedTime = model.CreatedTime,
                UpdatedTime = model.UpdatedTime,
                LastUsedTime = model.LastUsedTime,
                LicensePlate = model.LicensePlate,
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
