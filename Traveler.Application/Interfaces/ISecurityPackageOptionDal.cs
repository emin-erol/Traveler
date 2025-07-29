using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CarFeatureDtos;
using Traveler.Application.Dtos.SecurityPackageOptionDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface ISecurityPackageOptionDal : IGenericDal<SecurityPackageOption>
    {
        Task<List<ResultSecurityPackageOptionDto>> GetSecurityPackageOptionsBySecurityPackageId(int securityPackageId);
    }
}
