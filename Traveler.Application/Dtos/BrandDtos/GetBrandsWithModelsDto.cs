using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.ModelDtos;

namespace Traveler.Application.Dtos.BrandDtos
{
    public class GetBrandsWithModelsDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public List<string> ModelNames { get; set; }
    }
}
