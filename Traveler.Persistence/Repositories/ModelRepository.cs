using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.ModelDtos;
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
    }
}
