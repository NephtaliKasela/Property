using Property.Models;

namespace Property.DTOs.Product.ProductRealEstate
{
    public class GetProductRealEstateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }

        // Other properties specific to the product
        public string Category { get; set; }
        public int Room { get; set; }
        public string Address { get; set; }
        public DateOnly YearOfConstruction { get; set; }
        public bool Availability { get; set; }
        public DateTime PublicationDate { get; set; }

        // Foreign Keys
        public Models.Country Country { get; set; }
        public Models.City? City { get; set; }
        public Models.RentRealEstate? Rent { get; set; }
        public Models.SellRealEstate? Sell { get; set; }
        public Models.PropertyTypeRealEstate PropertyTypeRealEstate { get; set; }
		public List<Models.Images.ProductImageRealEstate>? ProductImages { get; set; }
		public Models.Agent Agent { get; set; }
		public List<Models.Reservation>? Reservations { get; set; }
	}
}
