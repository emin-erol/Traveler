using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.ModelFeatureDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class ModelFeatureRepository : GenericRepository<ModelFeature>, IModelFeatureDal
    {
        private readonly TravelerDbContext _context;
        public ModelFeatureRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(ModelFeature entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<ModelFeature>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<ModelFeature> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(ModelFeature entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(ModelFeature entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<ModelFeatureDto>> GetModelFeaturesByModelId(int modelId)
        {
            var carFeatures = await _context.ModelFeatures
                .Where(mf => mf.ModelId == modelId)
                .Select(mf => new ModelFeatureDto
                {
                    ModelFeatureId = mf.ModelFeatureId,
                    Available = mf.Available,
                    ModelId = mf.ModelId,
                    FeatureId = mf.FeatureId
                })
                .ToListAsync();

            return carFeatures;
        }
    }
}
