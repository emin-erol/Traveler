using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class ModelPricing
    {
        public int ModelPricingId { get; set; }
        public decimal Amount { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        public int PricingId { get; set; }
        public Pricing Pricing { get; set; }
    }
}
