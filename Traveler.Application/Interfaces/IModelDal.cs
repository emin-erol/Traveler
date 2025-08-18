using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.ModelDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface IModelDal : IGenericDal<Model>
    {
        Task<List<GetModelsByBrandDto>> GetModelsByBrand(int brandId);
        Task<string> GetBrandNameByModelName(string modelName);
    }
}
