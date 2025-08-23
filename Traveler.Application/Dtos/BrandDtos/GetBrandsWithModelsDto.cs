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
        public BrandDto Brand { get; set; }
        public List<ModelDto> Models { get; set; }
    }
}
