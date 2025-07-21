using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.CarPricingViewModels
{
    public class CreateCarPricingViewModel
    {
        public decimal Amount { get; set; }
        public int CarId { get; set; }
        public int PricingId { get; set; }
    }
}
