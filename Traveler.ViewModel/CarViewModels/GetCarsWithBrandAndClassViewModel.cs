using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.ModelViewModels;

namespace Traveler.ViewModel.CarViewModels
{
    public class GetCarsWithBrandAndClassViewModel
    {
        public int CarId { get; set; }
        public string StockNumber { get; set; }
        public ResultModelViewModel Model { get; set; }
    }
}
