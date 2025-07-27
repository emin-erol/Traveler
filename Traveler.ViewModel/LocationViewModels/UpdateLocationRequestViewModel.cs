using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.LocationAvailabilityViewModels;

namespace Traveler.ViewModel.LocationViewModels
{
    public class UpdateLocationRequestViewModel
    {
        public UpdateLocationViewModel Dto { get; set; }
        public List<UpdateLocationAvailabilityViewModel> LaDtos { get; set; }
    }
}
