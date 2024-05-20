namespace Property.DTOs.Reservation
{
    public class UpdateReservationDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int NumberOfPeople { get; set; }
        public string NumberOfGuest { get; set; }
        public DateOnly Arrival{ get; set; }
        public DateOnly Departure { get; set; }

        // Foreign key
        public Models.ApplicationUser applicationUser { get; set; }
        public int ProductRealEstateId { get; set; }
    }
}
