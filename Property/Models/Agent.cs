using Microsoft.AspNetCore.Identity;
using Property.Models.Products;

namespace Property.Models
{
	public class Agent
	{
		public int Id { get; set; }
		public string Name { get; set; }	
		public string Profession { get; set; }
		//***
		public double Debt { get; set; }
		public DateTime RegistrationDate { get; set; }
		//***
		public ApplicationUser ApplicationUser { get; set; }
		public List<ProductRealEstate>? ProductsRealEstate { get; set; }
	}
}