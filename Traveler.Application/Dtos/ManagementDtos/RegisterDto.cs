using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Application.Dtos.ManagementDtos
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly DriverLicenseDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordCheck { get; set; }
        public string Address { get; set; }
    }
}
