using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly DriverLicenseDate { get; set; }
        public string VerificationCode { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
