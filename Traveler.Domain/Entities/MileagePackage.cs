using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class MileagePackage
    {
        public int MileagePackageId { get; set; }
        public string PackageName { get; set; }
        public int PackageLimit { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
