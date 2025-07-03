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
    public class BrandRepository : GenericRepository<Brand>, IBrandDal
    {
        public BrandRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Brand entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Brand entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Brand entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
