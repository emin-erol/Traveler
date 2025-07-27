using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.LocationAvailabilityDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Interfaces
{
    public interface ILocationAvailabilityDal : IGenericDal<LocationAvailability>
    {
        Task<List<ResultLocationAvailabilityDto>> GetAllLocationAvailabilitiesByLocationId(int locationId);
    }
}
