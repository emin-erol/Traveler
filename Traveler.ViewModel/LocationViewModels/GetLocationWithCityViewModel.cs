using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.CityViewModels;

namespace Traveler.ViewModel.LocationViewModels
{
    public class GetLocationWithCityViewModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string? Description { get; set; }
        public ResultCityViewModel City { get; set; }
    }
}
