using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Application.Dtos.CarPricingDtos
{
    public class CarPricingDto
    {
        public int CarPricingId { get; set; }
        public decimal Amount { get; set; }

        public int CarId { get; set; }

        public int PricingId { get; set; }
    }
}
