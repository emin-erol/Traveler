using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.ModelPricingDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class ModelPricingRepository : GenericRepository<ModelPricing>, IModelPricingDal
    {
        private readonly TravelerDbContext _context;
        public ModelPricingRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(ModelPricing entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<ModelPricing>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<ModelPricing> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(ModelPricing entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(ModelPricing entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<ModelPricingDto>> GetModelPricingsByModelId(int modelId)
        {
            var modelPricings = await _context.ModelPricings
                .Where(mp => mp.ModelId == modelId)
                .Select(mp => new ModelPricingDto
                {
                    ModelPricingId = mp.ModelPricingId,
                    Amount = mp.Amount,
                    ModelId = mp.ModelId,
                    PricingId = mp.PricingId
                })
                .ToListAsync();

            return modelPricings;
        }
    }
}
