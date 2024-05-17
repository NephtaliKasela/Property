using Property.Models.Products;
using Property.Models;

namespace Property.DTOs.Agent
{
	public class AddAgentDTO
	{
        public string Name { get; set; }
        public string Profession { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
