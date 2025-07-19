using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CarFeatureDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface ICarFeatureDal : IGenericDal<CarFeature>
    {
        Task<List<CarFeatureDto>> GetCarFeaturesByCarId(int carId);
    }
}
