using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.BrandViewModels;
using Traveler.ViewModel.CarClassViewModels;
using Traveler.ViewModel.ModelFeatureViewModels;
using Traveler.ViewModel.ModelPricingViewModels;

namespace Traveler.ViewModel.ModelViewModels
{
    public class GetModelWithAllDetailsViewModel
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }
        public string CoverImageUrl { get; set; }
        public byte Seat { get; set; }
        public byte Luggage { get; set; }
        public string BigImageUrl { get; set; }
        public ResultBrandViewModel Brand { get; set; }
        public ResultCarClassViewModel CarClass { get; set; }
        public List<ResultModelFeatureViewModel> ModelFeatures { get; set; }
        public List<ResultModelPricingViewModel> ModelPricings { get; set; }
    }
}
