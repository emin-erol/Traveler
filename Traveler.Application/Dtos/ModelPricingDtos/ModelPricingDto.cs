using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Application.Dtos.ModelPricingDtos
{
    public class ModelPricingDto
    {
        public int ModelPricingId { get; set; }
        public decimal Amount { get; set; }

        public int ModelId { get; set; }

        public int PricingId { get; set; }
    }
}
