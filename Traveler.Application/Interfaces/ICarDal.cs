using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CarDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface ICarDal : IGenericDal<Car>
    {
        Task<List<GetCarWithBrandAndClassDto>> GetCarsWithBrandAndClass();
        Task<GetCarWithAllDetailsDto> GetCarWithAllDetails(int carId);
        Task<int> GetLastCarId();
    }
}
