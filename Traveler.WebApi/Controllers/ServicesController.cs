using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.ServiceDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceDal _serviceDal;

        public ServicesController(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var values = await _serviceDal.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var value = await _serviceDal.GetByIdAsync(id);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto model)
        {
            var service = new Service
            {
                Description = model.Description,
                IconUrl = model.IconUrl,
                Title = model.Title,
            };

            await _serviceDal.CreateAsync(service);

            return Ok("Service information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(UpdateServiceDto model)
        {
            var service = new Service
            {
                ServiceId = model.ServiceId,
                Description = model.Description,
                IconUrl = model.IconUrl,
                Title = model.Title,
            };

            await _serviceDal.UpdateAsync(service);

            return Ok("Service information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveService(int id)
        {
            var value = await _serviceDal.GetByIdAsync(id);

            await _serviceDal.RemoveAsync(value);

            return Ok("Service information has been removed.");
        }
    }
}
