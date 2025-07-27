using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.CityViewModels;
using Traveler.ViewModel.LocationAvailabilityViewModels;

namespace Traveler.ViewModel.LocationViewModels
{
    public class GetLocationWithCityAndAvailabilityViewModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string? Description { get; set; }
        public ResultCityViewModel City { get; set; }
        public List<ResultLocationAvailabilityViewModel> LocationAvailabilities { get; set; }
    }
}
