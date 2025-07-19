using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CarDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarDal
    {
        private readonly TravelerDbContext _context;
        public CarRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
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

        public async Task<List<GetCarWithBrandAndClassDto>> GetCarsWithBrandAndClass()
        {
            var result = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.CarClass)
                .Select(c => new GetCarWithBrandAndClassDto
                {
                    CarId = c.CarId,
                    StockNumber = c.StockNumber,
                    Model = c.Model,
                    BrandId = c.BrandId,
                    BrandName = c.Brand.Name,
                    ClassId = c.CarClassId,
                    ClassName = c.CarClass.ClassName
                })
                .ToListAsync();

            return result;
        }

        public async Task<GetCarWithAllDetailsDto> GetCarWithAllDetails(int carId)
        {
            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.CarClass)
                .FirstOrDefaultAsync(c => c.CarId == carId);

            var featureNames = await _context.CarFeatures
                .Where(cf => cf.CarId == carId)
                .Select(cf => cf.Feature.FeatureName)
                .ToListAsync();

            if (car == null)
                return null!;

            return new GetCarWithAllDetailsDto
            {
                CarId = car.CarId,
                StockNumber = car.StockNumber,
                Model = car.Model,
                Year = car.Year,
                CoverImageUrl = car.CoverImageUrl,
                Mileage = car.Mileage,
                Transmission = car.Transmission,
                Seat = car.Seat,
                Luggage = car.Luggage,
                Fuel = car.Fuel,
                BigImageUrl = car.BigImageUrl,
                Description = car.Description,
                Status = car.Status,
                FeatureNames = featureNames,
                Brand = new Brand
                {
                    BrandId = car.Brand.BrandId,
                    Name = car.Brand.Name
                },
                CarClass = new CarClass
                {
                    CarClassId = car.CarClass.CarClassId,
                    ClassName = car.CarClass.ClassName
                }
            };
        }

    }
}
