using Property.Models.Products;

namespace Property.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set;}
        //public string PhoneNumber { get; set;}
        public int NumberOfPeople { get; set;}
        public double Amount { get; set;}
        public double ReservationFee { get; set;}
        public DateOnly Arrival { get; set;}
        public DateOnly Departure { get; set;}
        public DateTime Date { get; set;}

        // Foreign key
        public ApplicationUser applicationUser { get; set;}
        public ProductRealEstate ProductRealEstate { get; set;}
    }
}
