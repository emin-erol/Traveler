using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
