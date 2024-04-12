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
        public DateTime YearOfConstruction { get; set; }
        public bool Availability { get; set; }
        public DateTime PublicationDate { get; set; }

        // Foreign Keys
        public Models.Country Country { get; set; }
        public Models.City? City { get; set; }
        public Models.Subcategories.SubcategoryRealEstate SubcategoryRealEstate { get; set; }
		public Models.TransactionType TransactionType { get; set; }
		public List<Models.Images.ProductImageRealEstate>? ProductImages { get; set; }
		public Models.Agent Agent { get; set; }
	}
}
