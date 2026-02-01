using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.LocationViewModels
{
    public class CreateLocationViewModel
    {
        public string LocationName { get; set; }
        public string? Description { get; set; }
        public int CityId { get; set; }
    }
}
