using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.ModelPricingViewModels
{
    public class CreateModelPricingViewModel
    {
        public decimal Amount { get; set; }
        public int ModelId { get; set; }
        public int PricingId { get; set; }
    }
}
