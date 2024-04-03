using Property.Models.Products;
using Property.Models;

namespace Property.DTOs.Country
{
	public class AddCountryDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string ContinentId { get; set; } = string.Empty;
	}
}
