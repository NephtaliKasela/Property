using Property.Models.Products;

namespace Property.Models
{
    public class RentRealEstate
    {
        public int Id { get; set; }
        public int ProductRealEstateId { get; set; }
        public ProductRealEstate ProductRealEstate { get; set; }
        public RentRealEstatePerDay? RentRealEstatePerDay { get; set; } // Day
        public RentRealEstatePerMonth? RentRealEstatePerMounth { get; set; } // Mounth
    }

    public class RentRealEstatePerDay
    {
        public int Id { get; set; }
        public int RentRealEstateId { get; set; }
        public RentRealEstate RentRealEstate { get; set; }
        public int NumberOfPoeple { get; set; }
        public int AdditionalPerson { get; set; }
        public double AdditionalPrice { get; set; }
        public double Debt { get; set; }
        public double Penality { get; set; }
    }

    public class RentRealEstatePerMonth
    {
        public int Id { get; set; }
        public int RentRealEstateId { get; set; }
        public RentRealEstate RentRealEstate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int Days { get; set; }
    }
}
