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
using Traveler.Application.Dtos.ModelDtos;
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
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
                .Include(c => c.Model.CarClass)
                .Select(c => new GetCarWithBrandAndClassDto
                {
                    CarId = c.CarId,
                    StockNumber = c.StockNumber,
                    Model = new ModelDto
                    {
                        ModelId = c.Model.ModelId,
                        ModelName = c.Model.ModelName,
                        ModelDescription = c.Model.ModelDescription,
                        CoverImageUrl = c.Model.CoverImageUrl,
                        Seat = c.Model.Seat,
                        Luggage = c.Model.Luggage,
                        BigImageUrl = c.Model.BigImageUrl,
                        Brand = new BrandDto
                        {
                            BrandId = c.Model.Brand.BrandId,
                            Name = c.Model.Brand.Name
                        },
                        CarClass = new CarClassDto
                        {
                            CarClassId = c.Model.CarClass.CarClassId,
                            ClassName = c.Model.CarClass.ClassName
                        }
                    }
                })
                .ToListAsync();

            return result;
        }

        public async Task<GetCarWithAllDetailsDto> GetCarWithAllDetails(int carId)
        {
            var car = await _context.Cars
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
                .Include(c => c.Model.CarClass)
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
                Year = car.Year,
                Mileage = car.Mileage,
                Transmission = car.Transmission,
                Fuel = car.Fuel,
                Description = car.Description,
                Status = car.Status,
                FeatureNames = featureNames,
                LocationName = car.Location.LocationName,
                CreatedTime = car.CreatedTime,
                LastUsedTime = car.LastUsedTime,
                UpdatedTime = car.UpdatedTime,
                LicensePlate = car.LicensePlate,
                Model = new ModelDto
                {
                    ModelId = car.Model.ModelId,
                    ModelName = car.Model.ModelName,
                    ModelDescription = car.Model.ModelDescription,
                    CoverImageUrl = car.Model.CoverImageUrl,
                    Seat = car.Model.Seat,
                    Luggage = car.Model.Luggage,
                    BigImageUrl = car.Model.BigImageUrl,
                    Brand = new BrandDto
                    {
                        BrandId = car.Model.Brand.BrandId,
                        Name = car.Model.Brand.Name
                    },
                    CarClass = new CarClassDto
                    {
                        CarClassId = car.Model.CarClass.CarClassId,
                        ClassName = car.Model.CarClass.ClassName
                    }
                }
            };
        }

        public async Task<List<GetCarWithAllDetailsDto>> GetCarsWithAllDetailsByLocation(int locationId)
        {
            var cars = await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
                .Include(c => c.Model.CarClass)
                .Include(c => c.Location)
                .Include(c => c.CarFeatures).ThenInclude(cf => cf.Feature)
                .Include(c => c.CarPricings).ThenInclude(cp => cp.Pricing)
                .Where(c => c.LocationId == locationId && c.Status == 1)
                .ToListAsync();

            var filteredCars = cars
                .GroupBy(c => new { c.Model.Brand.BrandId, c.Model })
                .Select(g => g.OrderBy(c => c.LastUsedTime).First())
                .ToList();

            var result = filteredCars.Select(c => new GetCarWithAllDetailsDto
            {
                CarId = c.CarId,
                StockNumber = c.StockNumber,
                Year = c.Year,
                Mileage = c.Mileage,
                Transmission = c.Transmission,
                Fuel = c.Fuel,
                Description = c.Description,
                Status = c.Status,
                LicensePlate = c.LicensePlate,
                Model = new ModelDto
                {
                    ModelId = c.Model.ModelId,
                    ModelName = c.Model.ModelName,
                    ModelDescription = c.Model.ModelDescription,
                    CoverImageUrl = c.Model.CoverImageUrl,
                    Seat = c.Model.Seat,
                    Luggage = c.Model.Luggage,
                    BigImageUrl = c.Model.BigImageUrl,
                    Brand = new BrandDto
                    {
                        BrandId = c.Model.Brand.BrandId,
                        Name = c.Model.Brand.Name
                    },
                    CarClass = new CarClassDto
                    {
                        CarClassId = c.Model.CarClass.CarClassId,
                        ClassName = c.Model.CarClass.ClassName
                    }
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
