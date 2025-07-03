using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Domain.Entities;

namespace Traveler.Application.Dtos.CarFeatureDtos
{
    public class CreateCarFeatureDto
    {
        public int CarFeatureId { get; set; }
        public bool Available { get; set; }

        public int CarId { get; set; }

        public int FeatureId { get; set; }
    }
}
