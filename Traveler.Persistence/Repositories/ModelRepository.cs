using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.BrandDtos;
using Traveler.Application.Dtos.CarClassDtos;
using Traveler.Application.Dtos.ModelDtos;
using Traveler.Application.Dtos.ModelFeatureDtos;
using Traveler.Application.Dtos.ModelPricingDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class ModelRepository : GenericRepository<Model>, IModelDal
    {
        private readonly TravelerDbContext _context;
        public ModelRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(Model entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Model>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Model> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Model entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Model entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<GetModelsByBrandDto>> GetModelsByBrand(int brandId)
        {
            var result = await _context.Models
                .Where(x => x.BrandId == brandId)
                .Select(x => new GetModelsByBrandDto
                {
                    ModelId = x.ModelId,
                    ModelName = x.ModelName
                })
                .ToListAsync();

            return result;
        }

        public async Task<string> GetBrandNameByModelName(string modelName)
        {
            var result = await (from m in _context.Models
                                join b in _context.Brands on m.BrandId equals b.BrandId
                                where m.ModelName == modelName
                                select b.Name)
                        .FirstOrDefaultAsync();

            return result!;
        }

        public async Task<GetModelWithAllDetails> GetModelWithAllDetails(int modelId)
        {
            var model = await _context.Models
                .Include(m => m.Brand)
                .Include(m => m.CarClass)
                .Include(m => m.ModelFeatures)
                .Include(m => m.ModelPricings)
                .FirstOrDefaultAsync(m => m.ModelId == modelId);

            if (model == null)
                return null!;

            var result = new GetModelWithAllDetails
            {
                ModelId = model.ModelId,
                ModelName = model.ModelName,
                ModelDescription = model.ModelDescription,
                CoverImageUrl = model.CoverImageUrl,
                BigImageUrl = model.BigImageUrl,
                Seat = model.Seat,
                Luggage = model.Luggage,
                Brand = new BrandDto
                {
                    BrandId = model.Brand.BrandId,
                    Name = model.Brand.Name
                },
                CarClass = new CarClassDto
                {
                    CarClassId = model.CarClass.CarClassId,
                    ClassName = model.CarClass.ClassName
                },
                ModelFeatures = model.ModelFeatures.Select(f => new ModelFeatureDto
                {
                    ModelFeatureId = f.ModelFeatureId,
                    Available = f.Available,
                    ModelId = f.ModelId,
                    FeatureId = f.FeatureId
                }).ToList(),
                ModelPricings = model.ModelPricings.Select(p => new ModelPricingDto
                {
                    ModelPricingId = p.ModelPricingId,
                    Amount = p.Amount,
                    ModelId = p.ModelId,
                    PricingId = p.PricingId
                }).ToList()
            };

            return result;
        }

        public async Task<List<GetModelWithAllDetails>> GetAllModelsWithDetailsByLocation(int locationId)
        {
            var modelIdsWithCars = await _context.Cars
                .Where(c => c.LocationId == locationId)
                .Select(c => c.ModelId)
                .Distinct()
                .ToListAsync();

            if (!modelIdsWithCars.Any())
                return new List<GetModelWithAllDetails>();

            var models = await _context.Models
                .Include(m => m.Brand)
                .Include(m => m.CarClass)
                .Include(m => m.ModelFeatures)
                .Include(m => m.ModelPricings)
                .Where(m => modelIdsWithCars.Contains(m.ModelId))
                .ToListAsync();

            var result = models.Select(m => new GetModelWithAllDetails
            {
                ModelId = m.ModelId,
                ModelName = m.ModelName,
                ModelDescription = m.ModelDescription,
                CoverImageUrl = m.CoverImageUrl,
                BigImageUrl = m.BigImageUrl,
                Seat = m.Seat,
                Luggage = m.Luggage,

                Brand = new BrandDto
                {
                    BrandId = m.Brand.BrandId,
                    Name = m.Brand.Name
                },
                CarClass = new CarClassDto
                {
                    CarClassId = m.CarClass.CarClassId,
                    ClassName = m.CarClass.ClassName
                },
                ModelFeatures = m.ModelFeatures.Select(f => new ModelFeatureDto
                {
                    ModelFeatureId = f.ModelFeatureId,
                    Available = f.Available,
                    ModelId = f.ModelId,
                    FeatureId = f.FeatureId
                }).ToList(),
                ModelPricings = m.ModelPricings.Select(p => new ModelPricingDto
                {
                    ModelPricingId = p.ModelPricingId,
                    Amount = p.Amount,
                    ModelId = p.ModelId,
                    PricingId = p.PricingId
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<int> GetMostSuitableCarIdByModelId(int modelId, int locationId, DateOnly pickUpDate, DateOnly dropOffDate)
        {
            var availableCars = await _context.Cars
                .Where(x =>
                    x.ModelId == modelId &&
                    x.LocationId == locationId &&
                    x.Status == 1)
                .ToListAsync();

            if (!availableCars.Any())
                return 0; // veya throw

            var suitableCars = new List<Car>();

            foreach (var car in availableCars)
            {
                bool hasConflict = await _context.Reservations.AnyAsync(r =>
                    r.CarId == car.CarId &&
                    (
                        pickUpDate < r.DropOffDate &&
                        dropOffDate > r.PickUpDate
                    )
                );

                if (!hasConflict)
                {
                    suitableCars.Add(car);
                }
            }

            if (!suitableCars.Any())
                return 0; // veya throw

            // 3️⃣ En uzun süredir kullanılmayan aracı seç
            var selectedCar = suitableCars
                .OrderBy(x => x.LastUsedTime)
                .First();

            return selectedCar.CarId;
        }
    }
}
