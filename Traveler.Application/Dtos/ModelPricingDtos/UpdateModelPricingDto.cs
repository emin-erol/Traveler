using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Domain.Entities;

namespace Traveler.Application.Dtos.ModelPricingDtos
{
    public class UpdateModelPricingDto
    {
        public int ModelPricingId { get; set; }
        public decimal Amount { get; set; }

        public int ModelId { get; set; }

        public int PricingId { get; set; }
    }
}
