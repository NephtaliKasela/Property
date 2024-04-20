using Property.Models.Products;

namespace Property.DTOs.Reservation
{
    public class GetReservationDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int NumberOfPeople { get; set; }
        public string NumberOfGuest { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }

        // Foreign key
        public Models.ApplicationUser applicationUser { get; set; }
        public ProductRealEstate ProductRealEstate { get; set; }
    }
}
