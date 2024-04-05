using Property.Models.Products;
using Property.Models;

namespace Property.DTOs.Agent
{
	public class UpdateAgentDTO
	{
		public int Id { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public List<ProductRealEstate>? ProductsRealEstate { get; set; }
	}
}
