using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class PackageOption
    {
        public int PackageOptionId { get; set; }
        public string OptionName { get; set; }
        public List<SecurityPackageOption> SecurityPackageOptions { get; set; }
    }
}
