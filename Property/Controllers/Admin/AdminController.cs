using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property.DTOs.Actions;
using Property.Services.AgentServices;
using Property.Services.ProductService.ProductServicesRealEstate;
using Property.Services.UserApplicationServices;

namespace Property.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
        private readonly IApplicationUserServices _applicationUserServices;
		private readonly IAgentServices _agentServices;
		private readonly IProductServicesRealEstate _productServicesRealEstate;

        public AdminController(IApplicationUserServices applicationUserServices, IAgentServices agentServices, IProductServicesRealEstate productServicesRealEstate)
        {
            _applicationUserServices = applicationUserServices;
			_agentServices = agentServices;
			_productServicesRealEstate = productServicesRealEstate;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
		{
            var products = await _productServicesRealEstate.GetAllProducts();
            var users = await _applicationUserServices.GetAllUsers();
            var agents = await _agentServices.GetAllAgents();
            var v = new AdminDashboard_action();

            v.Products = products.Data;
            v.Users = users.Data;
            v.Agents = agents.Data;

			return View(v);
		}
	}
}
