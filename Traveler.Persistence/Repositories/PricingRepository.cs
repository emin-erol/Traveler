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
    public class PricingRepository : GenericRepository<Pricing>, IPricingDal
    {
        public PricingRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Pricing entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Pricing>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Pricing> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Pricing entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Pricing entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
