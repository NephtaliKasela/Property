using Property.DTOs.Agent;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.UserApplication;

namespace Property.DTOs.Actions
{
    public class AdminDashboard_action
    {
        public List<GetProductRealEstateDTO> Products { get; set; }
        public List<GetApplicationUserDTO> Users { get; set; }
        public List<GetAgentDTO> Agents { get; set; }

        public AdminDashboard_action() 
        { 
            Products = new List<GetProductRealEstateDTO>();
            Users = new List<GetApplicationUserDTO>();
            Agents = new List<GetAgentDTO>();
        }
    }
}
