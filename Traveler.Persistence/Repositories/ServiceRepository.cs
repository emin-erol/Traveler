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
    public class ServiceRepository : GenericRepository<Service>, IServiceDal
    {
        public ServiceRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Service entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Service entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Service entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
