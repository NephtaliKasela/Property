using Property.Models.Products;

namespace Property.DTOs.Images
{
	public class AddProductImageRealEstateDTO
	{
		public int productId {  get; set; }
		public List<IFormFile> files { get; set; }

	}
}
