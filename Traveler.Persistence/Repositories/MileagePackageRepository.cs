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
    public class MileagePackageRepository : GenericRepository<MileagePackage>, IMileagePackageDal
    {
        public MileagePackageRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(MileagePackage entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<MileagePackage>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<MileagePackage> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(MileagePackage entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(MileagePackage entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
