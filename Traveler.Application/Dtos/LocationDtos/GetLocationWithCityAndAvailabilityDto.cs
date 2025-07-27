using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Application.Dtos.CityDtos;
using Traveler.Application.Dtos.LocationAvailabilityDtos;
using Traveler.Domain.Entities;

namespace Traveler.Application.Dtos.LocationDtos
{
    public class GetLocationWithCityAndAvailabilityDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string? Description { get; set; }
        public ResultCityDto City { get; set; }
        public List<ResultLocationAvailabilityDto> LocationAvailabilities { get; set; }
    }
}
