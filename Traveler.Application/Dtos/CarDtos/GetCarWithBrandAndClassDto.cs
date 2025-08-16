using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.ModelDtos;

namespace Traveler.Application.Dtos.CarDtos
{
    public class GetCarWithBrandAndClassDto
    {
        public int CarId { get; set; }
        public string StockNumber { get; set; }
        public ModelDto Model { get; set; }
    }
}
