using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Application.Dtos.LocationAvailabilityDtos
{
    public class CreateLocationAvailabilityDto
    {
        public int DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
        public int LocationId { get; set; }
    }
}
