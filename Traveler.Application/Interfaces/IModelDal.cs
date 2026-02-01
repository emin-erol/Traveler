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
        Task<GetModelWithAllDetails> GetModelWithAllDetails(int modelId);
        Task<List<GetModelWithAllDetails>> GetAllModelsWithDetailsByLocation(int locationId);
        Task<int> GetMostSuitableCarIdByModelId(int modelId, int locationId, DateOnly pickUpDate, DateOnly dropOffDate);
    }
}
