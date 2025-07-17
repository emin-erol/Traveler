using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Application.Dtos.CarDtos
{
    public class GetCarWithBrandAndClassDto
    {
        public int CarId { get; set; }
        public string StockNumber { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string Model { get; set; }
    }
}
