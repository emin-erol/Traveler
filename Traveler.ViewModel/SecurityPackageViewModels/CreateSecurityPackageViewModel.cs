using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.SecurityPackageViewModels
{
    public class CreateSecurityPackageViewModel
    {
        public string PackageName { get; set; }
        public decimal Amount { get; set; }
        public List<int> SelectedPackageOptionIds { get; set; }
    }
}
