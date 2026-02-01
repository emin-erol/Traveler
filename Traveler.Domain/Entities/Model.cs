using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class Model
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }
        public string CoverImageUrl { get; set; }
        public byte Seat { get; set; }
        public byte Luggage { get; set; }
        public string BigImageUrl { get; set; }
        public int BrandId { get; set; }
        public int CarClassId { get; set; }

        public List<Car> Cars { get; set; }
        public Brand Brand { get; set; }
        public CarClass CarClass { get; set; }

        public List<ModelFeature> ModelFeatures { get; set; }
        public List<ModelPricing> ModelPricings { get; set; }
    }
}
