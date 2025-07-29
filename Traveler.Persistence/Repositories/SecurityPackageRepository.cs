using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.SecurityPackageDtos;
using Traveler.Application.Dtos.SecurityPackageOptionDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;
using Traveler.Persistence.Context;

namespace Traveler.Persistence.Repositories
{
    public class SecurityPackageRepository : GenericRepository<SecurityPackage>, ISecurityPackageDal
    {
        private readonly TravelerDbContext _context;
        public SecurityPackageRepository(TravelerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(SecurityPackage entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<SecurityPackage>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<SecurityPackage> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(SecurityPackage entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(SecurityPackage entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<GetSecurityPackagesWithPackageOptionsDto>> GetAllSecurityPackagesWithPackageOptions()
        {
            var packages = await _context.SecurityPackages
                .Include(sp => sp.SecurityPackageOptions)
                    .ThenInclude(spo => spo.PackageOption)
                .ToListAsync();

            return packages.Select(p => new GetSecurityPackagesWithPackageOptionsDto
            {
                SecurityPackageId = p.SecurityPackageId,
                PackageName = p.PackageName,
                Amount = p.Amount,
                SecurityPackageOptions = p.SecurityPackageOptions.Select(spo => spo.PackageOption.OptionName).ToList()
            }).ToList();
        }
    }
}
