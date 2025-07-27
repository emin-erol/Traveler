using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.CityDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityDal _cityDal;

        public CitiesController(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCitys()
        {
            var values = await _cityDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            var value = await _cityDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CreateCityDto model)
        {
            var city = new City
            {
                CityName = model.CityName,
            };

            await _cityDal.CreateAsync(city);

            return Ok("City information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity(UpdateCityDto model)
        {
            var city = new City
            {
                CityId = model.CityId,
                CityName = model.CityName,
            };
            await _cityDal.UpdateAsync(city);

            return Ok("City information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCity(int id)
        {
            var value = await _cityDal.GetByIdAsync(id);

            await _cityDal.RemoveAsync(value);

            return Ok("City information has been removed.");
        }
    }
}
