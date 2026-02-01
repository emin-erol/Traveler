using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.ModelFeatureViewModels;
using Traveler.ViewModel.ModelPricingViewModels;

namespace Traveler.ViewModel.ModelViewModels
{
    public class CreateModelViewModel
    {
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }
        public string CoverImageUrl { get; set; }
        public byte Seat { get; set; }
        public byte Luggage { get; set; }
        public string BigImageUrl { get; set; }
        public int BrandId { get; set; }
        public int CarClassId { get; set; }
        public List<CreateModelFeatureViewModel> ModelFeatures { get; set; }
        public List<CreateModelPricingViewModel> ModelPricings { get; set; }
    }
}
