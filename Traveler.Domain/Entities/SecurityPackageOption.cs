using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class SecurityPackageOption
    {
        public int SecurityPackageOptionId { get; set; }

        public int PackageOptionId { get; set; }
        public PackageOption PackageOption { get; set; }

        public int SecurityPackageId { get; set; }
        public SecurityPackage SecurityPackage { get; set; }
    }
}
