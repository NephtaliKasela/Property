using Property.DTOs.Reservation;
using Property.Models;

namespace Property.Services.ReservationServices
{
    public interface IReservationServices
    {
        Task<ServiceResponse<List<GetReservationDTO>>> AddReservation(AddReservationDTO newReservation);
        Task<ServiceResponse<GetReservationDTO>> GetReservationById(int id);
        Task<ServiceResponse<List<GetReservationDTO>>> GetAllReservations();
        Task<ServiceResponse<GetReservationDTO>> UpdateReservation(UpdateReservationDTO updatedReservation);
        Task<ServiceResponse<List<GetReservationDTO>>> DeleteReservation(int id);
    }
}
