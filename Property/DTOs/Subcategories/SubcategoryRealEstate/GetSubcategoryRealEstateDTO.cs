using Property.Models.Products;

namespace Property.DTOs.Subcategories.SubcategoryRealEstate
{
	public class GetSubcategoryRealEstateDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		// Foreign Keys
		public Models.Category Category { get; set; }
		public List<ProductRealEstate>? ProductsRealEstate { get; set; }
	}
}
