using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.LocationAvailabilityViewModels
{
    public class UpdateLocationAvailabilityViewModel
    {
        public int LocationAvailabilityId { get; set; }
        public int DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
        public int LocationId { get; set; }
    }
}
