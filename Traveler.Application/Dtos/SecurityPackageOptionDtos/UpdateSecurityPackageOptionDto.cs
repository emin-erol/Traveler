using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Domain.Entities;

namespace Traveler.Application.Dtos.SecurityPackageOptionDtos
{
    public class UpdateSecurityPackageOptionDto
    {
        public int SecurityPackageOptionId { get; set; }

        public int PackageOptionId { get; set; }

        public int SecurityPackageId { get; set; }
    }
}
