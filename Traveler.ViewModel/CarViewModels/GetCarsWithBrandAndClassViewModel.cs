using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.CarViewModels
{
    public class GetCarsWithBrandAndClassViewModel
    {
        public int CarId { get; set; }
        public string StockNumber { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ClassName { get; set; }
    }
}
