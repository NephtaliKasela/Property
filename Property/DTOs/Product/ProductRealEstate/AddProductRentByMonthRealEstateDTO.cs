using Property.Models;

namespace Property.DTOs.Product.ProductRealEstate
{
	public class AddProductRentByMonthRealEstateDTO
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public double Price { get; set; }

		public int Room { get; set; }
		public string Address { get; set; }
		public DateOnly YearOfConstruction { get; set; }

        public int BedRoom { get; set; }
        public int BathRoom { get; set; }
        public int Garage { get; set; }
        public double Area { get; set; }

        // Foreign Keys
        public string CountryId { get; set; }
		public string CityId { get; set; }
		public string PropertyTypeId { get; set; }

		public ApplicationUser User { get; set; }
	}
}
