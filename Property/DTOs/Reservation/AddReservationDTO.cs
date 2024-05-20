using Property.Models.Products;

namespace Property.DTOs.Reservation
{
    public class AddReservationDTO
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfGuest { get; set; }
        public DateOnly Arrival { get; set; }
        public DateOnly Departure { get; set; }

        // Foreign key
        public Models.ApplicationUser applicationUser { get; set; }
        public int ProductRealEstateId { get; set; }
    }
}
