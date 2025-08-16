using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Domain.Entities
{
    public class CarClass
    {
        public int CarClassId { get; set; }
        public string ClassName { get; set; }

        public List<Model> Models { get; set; }
    }
}
