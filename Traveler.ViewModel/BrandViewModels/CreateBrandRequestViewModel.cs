using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.ViewModel.ModelViewModels;

namespace Traveler.ViewModel.BrandViewModels
{
    public class CreateBrandRequestViewModel
    {
        public CreateBrandViewModel BrandDto { get; set; }
        public List<CreateModelViewModel> ModelDtos { get; set; }
    }
}
