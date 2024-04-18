using Property.DTOs.Agent;
using Property.DTOs.Product.ProductRealEstate;

namespace Property.DTOs.Actions
{
	public class AgentDashboard_action
	{
		public GetAgentDTO Agent { get; set; }
		public List<GetProductRealEstateDTO> Products { get; set; }

        public AgentDashboard_action()
        {
            Agent = new GetAgentDTO();
            Products = new List<GetProductRealEstateDTO>();
        }
    }
}
