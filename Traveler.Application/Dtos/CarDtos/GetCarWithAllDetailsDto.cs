using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CarClassDtos;
using Traveler.Application.Dtos.CarPricingDtos;
using Traveler.Application.Dtos.ModelDtos;
using Traveler.Application.Dtos.PricingDtos;

namespace Traveler.Application.Dtos.CarDtos
{
    public class GetCarWithAllDetailsDto
    {
        public int CarId { get; set; }
        public string StockNumber { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int Transmission { get; set; }
        public int Fuel { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string LicensePlate { get; set; }
        public ModelDto Model { get; set; }
        public List<string> FeatureNames { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime LastUsedTime { get; set; }
        public List<PricingDto> Pricings { get; set; }
        public List<CarPricingDto> CarPricings { get; set; }
    }
}
