using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.BannerDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerDal _bannerDal;

        public BannersController(IBannerDal bannerDal)
        {
            _bannerDal = bannerDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanners()
        {
            var values = await _bannerDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannerById(int id)
        {
            var value = await _bannerDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerDto model)
        {
            var banner = new Banner
            {
                Description = model.Description,
                Title = model.Title,
                VideoDescription = model.VideoDescription,
                VideoUrl = model.VideoUrl,
            };

            await _bannerDal.CreateAsync(banner);

            return Ok("Banner information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBanner(UpdateBannerDto model)
        {
            var banner = new Banner
            {
                BannerId = model.BannerId,
                Title = model.Title,
                VideoDescription = model.VideoDescription,
                VideoUrl = model.VideoUrl,
            };

            await _bannerDal.UpdateAsync(banner);

            return Ok("Banner information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBanner(int id)
        {
            var value = await _bannerDal.GetByIdAsync(id);

            await _bannerDal.RemoveAsync(value);

            return Ok("Banner information has been removed.");
        }
    }
}
