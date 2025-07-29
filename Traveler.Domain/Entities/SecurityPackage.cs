using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class SecurityPackage
    {
        public int SecurityPackageId { get; set; }
        public string PackageName { get; set; }
        public decimal Amount { get; set; }
        public List<SecurityPackageOption> SecurityPackageOptions { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
