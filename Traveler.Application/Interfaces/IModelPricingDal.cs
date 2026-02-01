using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.ModelPricingDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface IModelPricingDal : IGenericDal<ModelPricing>
    {
        Task<List<ModelPricingDto>> GetModelPricingsByModelId(int modelId);
    }
}
