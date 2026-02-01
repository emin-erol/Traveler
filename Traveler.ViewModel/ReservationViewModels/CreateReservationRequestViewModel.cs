using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.ViewModel.ReservationViewModels
{
    public class CreateReservationRequestViewModel
    {
        public int ModelId { get; set; }
        public CreateReservationViewModel Reservation { get; set; }
    }
}
