using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.BrandDtos;
using Traveler.Application.Dtos.CarClassDtos;
using Traveler.Application.Dtos.ModelFeatureDtos;
using Traveler.Application.Dtos.ModelPricingDtos;

namespace Traveler.Application.Dtos.ModelDtos
{
    public class ModelDto
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }
        public string CoverImageUrl { get; set; }
        public byte Seat { get; set; }
        public byte Luggage { get; set; }
        public string BigImageUrl { get; set; }
        public BrandDto Brand { get; set; }
        public CarClassDto CarClass { get; set; }
        public List<ModelFeatureDto> ModelFeatures { get; set; }
        public List<ModelPricingDto> ModelPricings { get; set; }
    }
}
