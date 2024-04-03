using Property.Models.Products;

namespace Property.DTOs.Subcategories.SubcategoryRealEstate
{
	public class UpdateSubcategoryRealEstateDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
	}
}
