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
    public class FooterRepository : GenericRepository<Footer>, IFooterDal
    {
        public FooterRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Footer entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Footer>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Footer> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Footer entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Footer entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
