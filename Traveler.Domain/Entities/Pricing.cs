    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class Pricing
    {
        public int PricingId { get; set; }
        public int PricingType { get; set; }
        public int Quantity { get; set; }

        public List<CarPricing> CarPricings { get; set; }
    }
}
