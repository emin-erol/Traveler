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
    public class CarClassRepository : GenericRepository<CarClass>, ICarClassDal
    {
        public CarClassRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(CarClass entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<CarClass>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<CarClass> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(CarClass entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(CarClass entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
