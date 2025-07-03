using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.AboutDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutDal _aboutDal;

        public AboutsController(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAbouts()
        {
            var values = await _aboutDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(int id)
        {
            var value = await _aboutDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto model)
        {
            var about = new About
            {
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Title = model.Title,
            };

            await _aboutDal.CreateAsync(about);

            return Ok("About information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto model)
        {
            var about = new About
            {
                AboutId = model.AboutId,
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };
            await _aboutDal.UpdateAsync(about);

            return Ok("About information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAbout(int id)
        {
            var value = await _aboutDal.GetByIdAsync(id);

            await _aboutDal.RemoveAsync(value);

            return Ok("About information has been removed.");
        }
    }
}
