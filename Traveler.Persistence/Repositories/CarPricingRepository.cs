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
    public class CarPricingRepository : GenericRepository<CarPricing>, ICarPricingDal
    {
        public CarPricingRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(CarPricing entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<CarPricing>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<CarPricing> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(CarPricing entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(CarPricing entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
