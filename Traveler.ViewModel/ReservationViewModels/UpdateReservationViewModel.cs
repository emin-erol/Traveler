using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.ReservationViewModels
{
    public class UpdateReservationViewModel
    {
        public int ReservationId { get; set; }
        public string ReservationCode { get; set; }
        public int? PickUpLocationId { get; set; }
        public int? DropOffLocationId { get; set; }
        public DateOnly PickUpDate { get; set; }
        public DateOnly DropOffDate { get; set; }
        public TimeOnly PickUpTime { get; set; }
        public TimeOnly DropOffTime { get; set; }
        public int CarId { get; set; }
        public int? MileagePackageId { get; set; }
        public int SecurityPackageId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
