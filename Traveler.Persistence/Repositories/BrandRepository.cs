using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.BrandDtos;
using Traveler.Application.Dtos.CarClassDtos;
using Traveler.Application.Dtos.ModelDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandDal
    {
        private readonly TravelerDbContext _context;
        public BrandRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(Brand entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Brand entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Brand entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<BrandDto> GetBrandByModelId(int modelId)
        {
            var result = await _context.Models
                .Where(m => m.ModelId == modelId)
                .Select(m => new BrandDto
                {
                    BrandId = m.Brand.BrandId,
                    Name = m.Brand.Name
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<GetBrandsWithModelsDto>> GetBrandsWithModels()
        {
            var result = await _context.Brands
                .Include(b => b.Models)
                    .ThenInclude(m => m.CarClass)
                .Select(b => new GetBrandsWithModelsDto
                {
                    Brand = new BrandDto
                    {
                        BrandId = b.BrandId,
                        Name = b.Name
                    },
                    Models = b.Models.Select(m => new ModelDto
                    {
                        ModelId = m.ModelId,
                        ModelName = m.ModelName,
                        ModelDescription = m.ModelDescription,
                        CoverImageUrl = m.CoverImageUrl,
                        Seat = m.Seat,
                        Luggage = m.Luggage,
                        BigImageUrl = m.BigImageUrl,

                        CarClass = new CarClassDto
                        {
                            CarClassId = m.CarClass.CarClassId,
                            ClassName = m.CarClass.ClassName
                        },

                        Brand = null!
                    }).ToList()
                })
                .ToListAsync();

            return result;
        }

        public async Task<string> GetBrandNameById(int brandId)
        {
            var brandName = await _context.Brands
                .Where(x => x.BrandId == brandId)
                .Select(x => x.Name)
                .FirstOrDefaultAsync();

            return brandName!;
        }
    }
}
