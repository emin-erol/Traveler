using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CarFeatureDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class CarFeatureRepository : GenericRepository<CarFeature>, ICarFeatureDal
    {
        private readonly TravelerDbContext _context;
        public CarFeatureRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(CarFeature entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<CarFeature>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<CarFeature> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(CarFeature entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(CarFeature entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<CarFeatureDto>> GetCarFeaturesByCarId(int carId)
        {
            var carFeatures = await _context.CarFeatures
                .Where(cf => cf.CarId == carId)
                .Select(cf => new CarFeatureDto
                {
                    CarFeatureId = cf.CarFeatureId,
                    Available = cf.Available,
                    CarId = cf.CarId,
                    FeatureId = cf.FeatureId
                })
                .ToListAsync();

            return carFeatures;
        }
    }
}
