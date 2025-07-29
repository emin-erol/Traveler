using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Application.Dtos.SecurityPackageDtos
{
    public class UpdateSecurityPackageDto
    {
        public int SecurityPackageId { get; set; }
        public string PackageName { get; set; }
        public decimal Amount { get; set; }
    }
}
