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
    public class CarRepository : GenericRepository<Car>, ICarDal
    {
        public CarRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Car entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Car entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Car entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
