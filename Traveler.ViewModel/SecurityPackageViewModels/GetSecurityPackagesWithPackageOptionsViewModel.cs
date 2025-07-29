using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.SecurityPackageViewModels
{
    public class GetSecurityPackagesWithPackageOptionsViewModel
    {
        public int SecurityPackageId { get; set; }
        public string PackageName { get; set; }
        public decimal Amount { get; set; }
        public List<String> SecurityPackageOptions { get; set; }
    }
}
