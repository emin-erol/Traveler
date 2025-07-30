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
    public class ReservationRepository : GenericRepository<Reservation>, IReservationDal
    {
        public ReservationRepository(TravelerDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Reservation entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Reservation entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Reservation entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
