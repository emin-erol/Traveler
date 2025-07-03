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
    public class AboutRepository : GenericRepository<About>, IAboutDal
    {
        private readonly TravelerDbContext _context;
        public AboutRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(About entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<About>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(About entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(About entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
