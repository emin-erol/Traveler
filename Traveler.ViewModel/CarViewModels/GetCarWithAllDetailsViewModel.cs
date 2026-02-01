using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.ModelViewModels;
using Traveler.ViewModel.PricingViewModels;

namespace Traveler.ViewModel.CarViewModels
{
    public class GetCarWithAllDetailsViewModel
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
        public ResultModelViewModel Model { get; set; }
        public List<string> FeatureNames { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime LastUsedTime { get; set; }
        public List<ResultPricingViewModel> Pricings { get; set; }
    }
}
