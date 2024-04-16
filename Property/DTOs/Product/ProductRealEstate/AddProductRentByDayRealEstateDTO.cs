using Property.Models;

namespace Property.DTOs.Product.ProductRealEstate
{
	public class AddProductRentByDayRealEstateDTO
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public double Price { get; set; }

		public int Room { get; set; }
		public string Address { get; set; }
		public DateTime YearOfConstruction { get; set; }

		public int NumberOfPoeple { get; set; }
		public int AdditionalPerson { get; set; }
		public double AdditionalPrice { get; set; }

		// Foreign Keys
		public string CountryId { get; set; }
		public string CityId { get; set; }
		public string ProductSubCategoryId { get; set; }
		public ApplicationUser User { get; set; }
	}
}
