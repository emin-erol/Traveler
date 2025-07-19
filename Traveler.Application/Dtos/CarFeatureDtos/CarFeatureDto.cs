using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Application.Dtos.CarFeatureDtos
{
    public class CarFeatureDto
    {
        public int CarFeatureId { get; set; }
        public bool Available { get; set; }
        public int CarId { get; set; }
        public int FeatureId { get; set; }
    }
}
