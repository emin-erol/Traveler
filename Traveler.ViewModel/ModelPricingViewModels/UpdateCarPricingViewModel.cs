using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.ModelPricingViewModels
{
    public class UpdateCarPricingViewModel
    {
        public int CarPricingId { get; set; }
        public decimal Amount { get; set; }
        public int PricingId { get; set; }
        public int CarId { get; set; }
    }
}
