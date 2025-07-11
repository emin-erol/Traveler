using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Application.Dtos.PricingDtos
{
    public class CreatePricingDto
    {
        public string PricingType { get; set; }
        public decimal PricingDec { get; set; }
    }
}
