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
    public class FeatureRepository : GenericRepository<Feature>, IFeatureDal
    {
        public FeatureRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Feature entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Feature>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Feature> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Feature entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Feature entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
