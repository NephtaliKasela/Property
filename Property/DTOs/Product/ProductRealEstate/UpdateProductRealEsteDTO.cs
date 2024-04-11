using Property.Models;

namespace Property.DTOs.Product.ProductRealEstate
{
    public class UpdateProductRealEsteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }

        // Other properties specific to the product
        //public string Category { get; set; }
        public int Room { get; set; }
        public string Address { get; set; }
        public DateTime YearOfConstruction { get; set; }
        public bool Availability { get; set; }
        public DateTime PublicationDate { get; set; }

		// Foreign Keys
		public string CountryId { get; set; }
		public string CityId { get; set; }
		public string ProductSubCategoryId { get; set; }
		public string TransactionTypeId { get; set; }
		public ApplicationUser User { get; set; }
	}
}
