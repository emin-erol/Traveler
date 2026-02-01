using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CityDtos;
using Traveler.Application.Dtos.LocationDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationDal
    {
        private readonly TravelerDbContext _context;
        public LocationRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(Location entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Location>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Location entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Location entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<GetLocationWithCityDto>> GetLocationWithCity()
        {
            var locations = await _context.Locations
                .Include(x => x.City)
                .ToListAsync();

            var result = locations.Select(location => new GetLocationWithCityDto
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName,
                Description = location.Description,
                City = new ResultCityDto
                {
                    CityId = location.City.CityId,
                    CityName = location.City.CityName
                }
            }).ToList();

            return result;
        }
    }
}
