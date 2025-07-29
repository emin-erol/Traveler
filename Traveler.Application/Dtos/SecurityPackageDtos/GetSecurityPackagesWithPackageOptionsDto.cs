using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.PackageOptionDtos;
using Traveler.Application.Dtos.SecurityPackageOptionDtos;

namespace Traveler.Application.Dtos.SecurityPackageDtos
{
    public class GetSecurityPackagesWithPackageOptionsDto
    {
        public int SecurityPackageId { get; set; }
        public string PackageName { get; set; }
        public decimal Amount { get; set; }
        public List<String> SecurityPackageOptions { get; set; }
    }
}
