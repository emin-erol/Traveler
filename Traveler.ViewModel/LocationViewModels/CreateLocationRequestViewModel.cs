using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.LocationAvailabilityViewModels;

namespace Traveler.ViewModel.LocationViewModels
{
    public class CreateLocationRequestViewModel
    {
        public CreateLocationViewModel Dto { get; set; }
        public List<CreateLocationAvailabilityViewModel> LaDtos { get; set; }
    }
}
