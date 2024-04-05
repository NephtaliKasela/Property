using Property.DTOs.Agent;
using Property.DTOs.Agent;
using Property.Models;

namespace Property.Services.AgentServices
{
	public interface IAgentServices
	{
		Task<ServiceResponse<List<GetAgentDTO>>> GetAllAgents();
		Task<ServiceResponse<GetAgentDTO>> GetAgentById(int id);
		Task<ServiceResponse<List<GetAgentDTO>>> AddAgent(AddAgentDTO newAgent);
		Task<ServiceResponse<GetAgentDTO>> UpdateAgent(UpdateAgentDTO updatedAgent);
		Task<ServiceResponse<List<GetAgentDTO>>> DeleteAgent(int id);
	}
}
