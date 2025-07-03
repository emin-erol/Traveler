using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CoverImageUrl { get; set; }
        public int Mileage { get; set; }
        public int Transmission { get; set; }
        public byte Seat { get; set; }
        public byte Luggage { get; set; }
        public int Fuel { get; set; }
        public string BigImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CarClassId { get; set; }
        public CarClass CarClass { get; set; }

        public List<CarFeature> CarFeatures { get; set; }
        public List<CarPricing> CarPricings { get; set; }
    }
}
