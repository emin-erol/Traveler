using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.ModelFeatureDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface IModelFeatureDal : IGenericDal<ModelFeature>
    {
        Task<List<ModelFeatureDto>> GetModelFeaturesByModelId(int modelId);
    }
}
