using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.BrandViewModels
{
    public class GetBrandsWithModelsViewModel
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public List<string> ModelNames { get; set; }
    }
}
