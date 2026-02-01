using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Domain.Entities;

namespace Traveler.Application.Dtos.ModelFeatureDtos
{
    public class UpdateModelFeatureDto
    {
        public int ModelFeatureId { get; set; }
        public bool Available { get; set; }

        public int ModelId { get; set; }

        public int FeatureId { get; set; }
    }
}
