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
using Property.Services.ImageServices;
using Property.Services.OtherServices;

namespace Property.Controllers
{
	public class AgentController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IAgentServices _agentServices;
		private readonly IProductImageServicesRealEstate _productImageServicesRealEstate;
		private readonly IOtherServices _otherServices;

		public AgentController(UserManager<ApplicationUser> userManager, IAgentServices agentServices, IProductImageServicesRealEstate productImageServicesRealEstate, IOtherServices otherServices)
		{
			_userManager = userManager;
			_agentServices = agentServices;
			_productImageServicesRealEstate = productImageServicesRealEstate;
			_otherServices = otherServices;
		}

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
			//Get the current user
			ApplicationUser user = await _userManager.GetUserAsync(User);

			if (user != null)
			{
				//Get specific agent by user id
				var agent = await _agentServices.GetAgentByUserId(user.Id);
				if (agent.Data != null)
				{
					//Check if agent has products
					if(agent.Data.ProductsRealEstate.Count > 0)
					{
						//Go through each agent product and add all images related to it
						foreach(var product in  agent.Data.ProductsRealEstate)
						{
							//Get images
							var images = await _productImageServicesRealEstate.GetImageByProductId(product.Id);
							if(images.Data != null)
							{
								product.ProductImages = images.Data;
							}

							//Get transaction type
							var transactionType = await _otherServices.GetTransactionTyoeByProductRealEstateId(product.Id);
							if(transactionType.Data != null)
							{
								product.TransactionType = transactionType.Data;
							}
						}
					}
					return View(agent.Data);
				}
			}
			return RedirectToAction("AddAgent");
        }

        [Authorize]
        public async Task<IActionResult> AddAgent()
		{
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

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> SaveAddAgent(AddAgentDTO newAgent)
		{
            ApplicationUser user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				// Access user properties
				newAgent.ApplicationUser = user;
				newAgent.ApplicationUserId = user.Id;
				// ...

				await _agentServices.AddAgent(newAgent);
				return RedirectToAction("Dashboard");
			}

            return RedirectToAction("AddAgent");
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
