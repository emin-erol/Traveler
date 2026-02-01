using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class ModelFeature
    {
        public int ModelFeatureId { get; set; }
        public bool Available { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
