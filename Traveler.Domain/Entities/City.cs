using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public int CityName { get; set; }
        public List<Location> Locations { get; set; }
    }
}
