using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Domain.Entities;

namespace Traveler.Application.Dtos.LocationDtos
{
    public class CreateLocationDto
    {
        public string LocationName { get; set; }
        public string? Description { get; set; }
        public int CityId { get; set; }
    }
}
