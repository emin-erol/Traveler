using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.BrandDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface IBrandDal : IGenericDal<Brand>
    {
        Task<BrandDto> GetBrandByModelId(int modelId);
        Task<List<GetBrandsWithModelsDto>> GetBrandsWithModels();
    }
}
