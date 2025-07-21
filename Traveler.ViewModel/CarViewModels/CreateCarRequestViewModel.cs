using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.CarPricingViewModels;

namespace Traveler.ViewModel.CarViewModels
{
    public class CreateCarRequestViewModel
    {
        public CreateCarViewModel Dto { get; set; }
        public List<CreateCarPricingViewModel> CpDtos { get; set; }
    }
}
