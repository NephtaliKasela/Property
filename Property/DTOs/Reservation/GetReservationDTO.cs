using Property.Models.Products;

namespace Property.DTOs.Reservation
{
    public class GetReservationDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfGuest { get; set; }
        public DateOnly Arrival { get; set; }
        public DateOnly Departure { get; set; }
        public double Amount { get; set; }
        public double ReservationFee { get; set; }
        public DateTime Date { get; set; }

        // Foreign key
        public Models.ApplicationUser applicationUser { get; set; }
        public ProductRealEstate ProductRealEstate { get; set; }
    }
}
