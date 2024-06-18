using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Property.DTOs.Actions;
using Property.DTOs.Agent;
using Property.Models;
using Property.Services.AgentServices;
using Property.Services.CityServices;
using Property.Services.CountryServices;
using Property.Services.ImageServices;
using Property.Services.OtherServices;
using Property.Services.ProductService.ProductServicesRealEstate;

namespace Property.Controllers
{
	[Authorize]
	public class AgentController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAgentServices _agentServices;
		private readonly IProductServicesRealEstate _productServicesRealEstate;

		public AgentController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAgentServices agentServices, IProductServicesRealEstate productServicesRealEstate)
		{
			_userManager = userManager;
            _roleManager = roleManager;
            _agentServices = agentServices;
			_productServicesRealEstate = productServicesRealEstate;
		}

        [Authorize(Policy ="AgentRole")]
        [HttpGet]
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
					var AgentProducts = await _productServicesRealEstate.GetProductByAgentId(agent.Data.Id);

					var v = new AgentDashboard_action();
					v.Agent = agent.Data;
					v.Products = AgentProducts.Data;

					return View(v);
				}
			}
			return RedirectToAction("AddAgent");
        }

        [Authorize(Policy = "AgentRole")]
        [HttpGet]
		public async Task<IActionResult> Properties()
		{
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                //Get specific agent by user id
                var agent = await _agentServices.GetAgentByUserId(user.Id);
                if (agent.Data != null)
                {
                    var AgentProducts = await _productServicesRealEstate.GetProductByAgentId(agent.Data.Id);

                    var v = new AgentDashboard_action();
                    v.Agent = agent.Data;
                    v.Products = AgentProducts.Data;

                    return View(v);
                }
            }
            return RedirectToAction("AddAgent");
        }

        [Authorize(Policy = "AgentRole")]
        [HttpGet]
		public async Task<IActionResult> GetAgentProductRentByDay(int id)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                //Get specific agent by user id
                var agent = await _agentServices.GetAgentByUserId(user.Id);
                if (agent.Data != null)
                {
                    var agentProduct = await _productServicesRealEstate.GetProductById(id);
					if(agentProduct.Data != null)
					{
						return View(agentProduct.Data);
					}
                }
            }
            return RedirectToAction("AddAgent");
        }

        public IActionResult AddAgent()
		{
            return View();
		}

        [Authorize(Policy = "ManagerRole, AdminRole")]
        [HttpGet]
		public async Task<IActionResult> GetAgents()
		{
			var agents = await _agentServices.GetAllAgents();
			if(agents.Data is not null)
			{
				return View(agents.Data);
			}
			return View("Index", "Home");
		}

        [Authorize(Policy = "AgentRole, AdminRole")]
        [HttpGet]
		public async Task<IActionResult> UpdateAgent(int id)
		{
			var agent = await _agentServices.GetAgentById(id);

			return View(agent.Data);
		}

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

                string role = "Agent";
                var r = _roleManager.FindByNameAsync(role);
                if (r != null)
                {
                    if (!await _userManager.IsInRoleAsync(user, role))
                    {
                        var result = await _userManager.AddToRoleAsync(user, role);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Dashboard");
                        }
                    }
                }
            }

            return RedirectToAction("AddAgent");
        }

        [Authorize(Policy = "AgentRole")]
        [HttpPost]
		public async Task<IActionResult> SaveUpdateAgent(UpdateAgentDTO updatedAgent)
		{
			await _agentServices.UpdateAgent(updatedAgent);
			return RedirectToAction("GetAgent");
		}

        [Authorize(Policy = "AgentRole")]
        [HttpPost]
		public async Task<IActionResult> DeleteAgent(int id)
		{
			await _agentServices.DeleteAgent(id);
			return RedirectToAction("GetAgent");
		}
	}
}
