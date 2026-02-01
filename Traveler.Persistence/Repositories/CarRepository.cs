using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.BrandDtos;
using Traveler.Application.Dtos.CarClassDtos;
using Traveler.Application.Dtos.CarDtos;
using Traveler.Application.Dtos.ModelDtos;
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
                    BrandName = c.Model.Brand.Name,
                    ModelName = c.Model.ModelName,
                    ClassName = c.Model.CarClass.ClassName
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

        public async Task<int> GetLastCarId()
        {
            var lastCar = await _context.Cars
                .OrderByDescending(c => c.CarId)
                .FirstOrDefaultAsync();

            return lastCar?.CarId ?? 0;
        }
    }
}
