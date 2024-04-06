using Microsoft.AspNetCore.Identity;
using Property.Models.Products;

namespace Property.Models
{
	public class Agent
	{
		public int Id { get; set; }
		public string Name { get; set; }	
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public List<ProductRealEstate>? ProductsRealEstate { get; set; }
	}
}
