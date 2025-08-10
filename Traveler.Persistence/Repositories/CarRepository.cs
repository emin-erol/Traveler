using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.BrandDtos;
using Traveler.Application.Dtos.CarClassDtos;
using Traveler.Application.Dtos.CarDtos;
using Traveler.Application.Dtos.CarPricingDtos;
using Traveler.Application.Dtos.PricingDtos;
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
                .Include(c => c.Location)
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
                LocationName = car.Location.LocationName,
                CreatedTime = car.CreatedTime,
                LastUsedTime = car.LastUsedTime,
                UpdatedTime = car.UpdatedTime,
                Brand = new BrandDto
                {
                    BrandId = car.Brand.BrandId,
                    Name = car.Brand.Name
                },
                CarClass = new CarClassDto
                {
                    CarClassId = car.CarClass.CarClassId,
                    ClassName = car.CarClass.ClassName
                }
            };
        }

        public async Task<List<GetCarWithAllDetailsDto>> GetCarsWithAllDetailsByLocation(int locationId)
        {
            var cars = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.CarClass)
                .Include(c => c.Location)
                .Include(c => c.CarFeatures).ThenInclude(cf => cf.Feature)
                .Include(c => c.CarPricings).ThenInclude(cp => cp.Pricing)
                .Where(c => c.LocationId == locationId && c.Status == 1)
                .ToListAsync();

            var filteredCars = cars
                .GroupBy(c => new { c.BrandId, c.Model })
                .Select(g => g.OrderBy(c => c.LastUsedTime).First())
                .ToList();

            var result = filteredCars.Select(c => new GetCarWithAllDetailsDto
            {
                CarId = c.CarId,
                StockNumber = c.StockNumber,
                Model = c.Model,
                Year = c.Year,
                CoverImageUrl = c.CoverImageUrl,
                Mileage = c.Mileage,
                Transmission = c.Transmission,
                Seat = c.Seat,
                Luggage = c.Luggage,
                Fuel = c.Fuel,
                BigImageUrl = c.BigImageUrl,
                Description = c.Description,
                Status = c.Status,
                Brand = new BrandDto
                {
                    BrandId = c.Brand.BrandId,
                    Name = c.Brand.Name
                },
                CarClass = new CarClassDto
                {
                    CarClassId = c.CarClass.CarClassId,
                    ClassName = c.CarClass.ClassName
                },
                FeatureNames = c.CarFeatures?.Select(cf => cf.Feature.FeatureName).ToList() ?? new List<string>(),
                LocationName = c.Location?.LocationName!,
                CreatedTime = c.CreatedTime,
                UpdatedTime = c.UpdatedTime,
                LastUsedTime = c.LastUsedTime,
                Pricings = c.CarPricings?.Select(cp => new PricingDto
                {
                    PricingId = cp.Pricing.PricingId,
                    PricingType = cp.Pricing.PricingType,
                    Quantity = cp.Pricing.Quantity,
                }).ToList() ?? new List<PricingDto>(),
                CarPricings = c.CarPricings?.Select(cp => new CarPricingDto
                {
                    CarPricingId = cp.CarPricingId,
                    CarId = cp.CarId,
                    PricingId = cp.PricingId,
                    Amount = cp.Amount,
                }).ToList() ?? new List<CarPricingDto>()
            }).ToList();

            return result;
        }

    }
}
