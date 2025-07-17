using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.BrandViewModels;
using Traveler.ViewModel.CarClassViewModels;

namespace Traveler.ViewModel.CarViewModels
{
    public class CreateCarViewModel
    {
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
        public int BrandId { get; set; }
        public int CarClassId { get; set; }
    }
}
