using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.LocationDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationDal _locationDal;

        public LocationsController(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var values = await _locationDal.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var value = await _locationDal.GetByIdAsync(id);

            return Ok(value);
        }

        [HttpGet("GetLocationWithCity")]
        public async Task<IActionResult> GetLocationWithCity()
        {
            var values = await _locationDal.GetLocationWithCity();

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationDto model)
        {
            var location = new Location
            {
                LocationName = model.LocationName,
                CityId = model.CityId,
                Description = model.Description,
            };

            await _locationDal.CreateAsync(location);

            return Ok(location);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocation(UpdateLocationDto model)
        {
            var location = new Location
            {
                LocationId = model.LocationId,
                LocationName = model.LocationName,
                CityId = model.CityId,
                Description = model.Description,
            };

            await _locationDal.UpdateAsync(location);

            return Ok("Location information has been updated!");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLocation(int id)
        {
            var value = await _locationDal.GetByIdAsync(id);

            await _locationDal.RemoveAsync(value);

            return Ok("Location information has been removed!");
        }
    }
}
