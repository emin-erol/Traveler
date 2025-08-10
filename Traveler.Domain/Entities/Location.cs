using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string? Description { get; set; }
        public List<LocationAvailability> LocationAvailabilities { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public List<Reservation> PickUpReservation { get; set; }
        public List<Reservation> DropOffReservation { get; set; }
        public List<Car> Cars { get; set; }
    }
}
