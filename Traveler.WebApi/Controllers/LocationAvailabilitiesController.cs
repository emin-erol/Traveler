using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.LocationAvailabilityDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationAvailabilitiesController : ControllerBase
    {
        private readonly ILocationAvailabilityDal _locationAvailabilityDal;

        public LocationAvailabilitiesController(ILocationAvailabilityDal locationAvailabilityDal)
        {
            _locationAvailabilityDal = locationAvailabilityDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocationAvailabilitys()
        {
            var values = await _locationAvailabilityDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationAvailabilityById(int id)
        {
            var value = await _locationAvailabilityDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetAllLocationAvailabilitiesByLocationId/{locationId}")]
        public async Task<IActionResult> GetAllLocationAvailabilitiesByLocationId(int locationId)
        {
            var values = await _locationAvailabilityDal.GetAllLocationAvailabilitiesByLocationId(locationId);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocationAvailability(CreateLocationAvailabilityDto model)
        {
            var locationAvailability = new LocationAvailability
            {
                DayOfWeek = model.DayOfWeek,
                IsAvailable = model.IsAvailable,
                LocationId = model.LocationId,
            };

            await _locationAvailabilityDal.CreateAsync(locationAvailability);

            return Ok("LocationAvailability information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocationAvailability(UpdateLocationAvailabilityDto model)
        {
            var locationAvailability = new LocationAvailability
            {
                LocationAvailabilityId = model.LocationAvailabilityId,
                DayOfWeek = model.DayOfWeek,
                IsAvailable = model.IsAvailable,
                LocationId = model.LocationId,
            };
            await _locationAvailabilityDal.UpdateAsync(locationAvailability);

            return Ok("LocationAvailability information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLocationAvailability(int id)
        {
            var value = await _locationAvailabilityDal.GetByIdAsync(id);

            await _locationAvailabilityDal.RemoveAsync(value);

            return Ok("LocationAvailability information has been removed.");
        }
    }
}
