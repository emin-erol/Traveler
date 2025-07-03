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
    public class CarFeatureRepository : GenericRepository<CarFeature>, ICarFeatureDal
    {
        public CarFeatureRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(CarFeature entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<CarFeature>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<CarFeature> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(CarFeature entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(CarFeature entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
