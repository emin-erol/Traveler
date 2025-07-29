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
    public class PackageOptionRepository : GenericRepository<PackageOption>, IPackageOptionDal
    {
        public PackageOptionRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(PackageOption entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<PackageOption>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<PackageOption> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(PackageOption entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(PackageOption entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
