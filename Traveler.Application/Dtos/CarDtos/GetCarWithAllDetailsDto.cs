using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Domain.Entities;

namespace Traveler.Application.Dtos.CarDtos
{
    public class GetCarWithAllDetailsDto
    {
        public int CarId { get; set; }
        public string StockNumber { get; set; }
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
        public int Status { get; set; }
        public Brand Brand { get; set; }
        public CarClass CarClass { get; set; }
    }
}
