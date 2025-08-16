using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Application.Dtos.ReservationDtos;
using Traveler.Application.Interfaces;
using Traveler.Domain.Entities;

namespace Traveler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationDal _reservationDal;

        public ReservationsController(IReservationDal reservationDal)
        {
            _reservationDal = reservationDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var values = await _reservationDal.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var value = await _reservationDal.GetByIdAsync(id);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDto dto)
        {
            var reservation = new Reservation
            {
                CarId = dto.CarId,
                Description = dto.Description,
                DropOffDate = dto.DropOffDate,
                DropOffLocationId = dto.DropOffLocationId,
                DropOffTime = dto.DropOffTime,
                MileagePackageId = dto.MileagePackageId,
                PickUpDate = dto.PickUpDate,
                PickUpLocationId = dto.PickUpLocationId,
                PickUpTime = dto.PickUpTime,
                ReservationCode = dto.ReservationCode,
                SecurityPackageId = dto.SecurityPackageId,
                TotalAmount = dto.TotalAmount,
                CreatedTime = dto.CreatedTime,
                UpdatedTime = dto.UpdatedTime,
                UserId = dto.UserId,
            };

            await _reservationDal.CreateAsync(reservation);

            return Ok(reservation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDto dto)
        {
            var reservation = new Reservation
            {
                ReservationId = dto.ReservationId,
                CarId = dto.CarId,
                Description = dto.Description,
                DropOffDate = dto.DropOffDate,
                DropOffLocationId = dto.DropOffLocationId,
                DropOffTime = dto.DropOffTime,
                MileagePackageId = dto.MileagePackageId,
                PickUpDate = dto.PickUpDate,
                PickUpLocationId = dto.PickUpLocationId,
                PickUpTime = dto.PickUpTime,
                ReservationCode = dto.ReservationCode,
                SecurityPackageId = dto.SecurityPackageId,
                TotalAmount = dto.TotalAmount,
                CreatedTime = dto.CreatedTime,
                UpdatedTime = dto.UpdatedTime,
                UserId = dto.UserId,
            };

            await _reservationDal.UpdateAsync(reservation);

            return Ok(reservation);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveReservation(int id)
        {
            var value = await _reservationDal.GetByIdAsync(id);

            await _reservationDal.RemoveAsync(value);

            return Ok(value);
        }
    }
}
