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
    public class ModelRepository : GenericRepository<Model>, IModelDal
    {
        public ModelRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Model entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Model>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Model> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Model entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Model entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
