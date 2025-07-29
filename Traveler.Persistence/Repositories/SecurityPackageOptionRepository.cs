using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CarFeatureDtos;
using Traveler.Application.Dtos.SecurityPackageOptionDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class SecurityPackageOptionRepository : GenericRepository<SecurityPackageOption>, ISecurityPackageOptionDal
    {
        private readonly TravelerDbContext _context;
        public SecurityPackageOptionRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(SecurityPackageOption entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<SecurityPackageOption>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<SecurityPackageOption> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(SecurityPackageOption entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(SecurityPackageOption entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<ResultSecurityPackageOptionDto>> GetSecurityPackageOptionsBySecurityPackageId(int securityPackageId)
        {
            var securityPackageOptions = await _context.SecurityPackageOptions
                .Where(spo => spo.SecurityPackageId == securityPackageId)
                .Select(spo => new ResultSecurityPackageOptionDto
                {
                    SecurityPackageId = securityPackageId,
                    SecurityPackageOptionId = spo.SecurityPackageOptionId,
                    PackageOptionId = spo.PackageOptionId
                })
                .ToListAsync();

            return securityPackageOptions;
        }
    }
}
