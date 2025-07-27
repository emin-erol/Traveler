using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.LocationAvailabilityDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class LocationAvailabilityRepository : GenericRepository<LocationAvailability>, ILocationAvailabilityDal
    {
        private readonly TravelerDbContext _context;
        public LocationAvailabilityRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(LocationAvailability entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<LocationAvailability>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<LocationAvailability> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(LocationAvailability entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(LocationAvailability entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<ResultLocationAvailabilityDto>> GetAllLocationAvailabilitiesByLocationId(int locationId)
        {
            var result = await _context.LocationAvailabilities
                .Where(x => x.LocationId == locationId)
                .Select(x => new ResultLocationAvailabilityDto
                {
                    LocationId = locationId,
                    DayOfWeek = x.DayOfWeek,
                    LocationAvailabilityId = x.LocationAvailabilityId,
                    IsAvailable = x.IsAvailable,
                })
                .ToListAsync();

            return result;
        }
    }
}
