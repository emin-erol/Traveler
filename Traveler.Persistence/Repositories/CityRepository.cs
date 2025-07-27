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
    public class CityRepository : GenericRepository<City>, ICityDal
    {
        public CityRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(City entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(City entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(City entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
