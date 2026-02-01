using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.ModelFeatureViewModels
{
    public class CreateModelFeatureViewModel
    {
        public int ModelId { get; set; }
        public int FeatureId { get; set; }
        public bool Available { get; set; }
    }
}
