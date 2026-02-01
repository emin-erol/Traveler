using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.LocationDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface ILocationDal : IGenericDal<Location>
    {
        Task<List<GetLocationWithCityDto>> GetLocationWithCity();
    }
}
