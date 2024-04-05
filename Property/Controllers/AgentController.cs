using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Property.DTOs.Actions;
using Property.DTOs.Agent;
using Property.DTOs.City;
using Property.Models;
using Property.Services.AgentServices;
using Property.Services.CityServices;
using Property.Services.CountryServices;

namespace Property.Controllers
{
	public class AgentController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IAgentServices _agentServices;

		public AgentController(UserManager<ApplicationUser> userManager, IAgentServices agentServices)
		{
			_userManager = userManager;
			_agentServices = agentServices;
		}

		public async Task<IActionResult> AddAgent()
		{
			//var agents = await _agentServices.GetAllAgents();
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetAgents()
		{
			var agents = await _agentServices.GetAllAgents();
			return View(agents.Data);
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetAgent()
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				// Access user properties
				string userId = user.Id;
				string email = user.Email;
				// ...
			}
			var agents = await _agentServices.GetAgentById(1);
			return View(agents.Data);
		}

		public async Task<IActionResult> UpdateAgent(int id)
		{
			var agent = await _agentServices.GetAgentById(id);

			return View(agent.Data);
		}

		[HttpPost]
		public async Task<IActionResult> SaveAddAgent(AddAgentDTO newAgent)
		{
			await _agentServices.AddAgent(newAgent);

			return RedirectToAction("GetAgent");
		}

		[HttpPost]
		public async Task<IActionResult> SaveUpdateAgent(UpdateAgentDTO updatedAgent)
		{
			await _agentServices.UpdateAgent(updatedAgent);
			return RedirectToAction("GetAgent");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAgent(int id)
		{
			await _agentServices.DeleteAgent(id);
			return RedirectToAction("GetAgent");
		}
	}
}
