using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property.DTOs.Actions;
using Property.Services.AgentServices;
using Property.Services.ProductService.ProductServicesRealEstate;
using Property.Services.ReservationServices;
using Property.Services.UserApplicationServices;

namespace Property.Controllers.Admin
{
	[Authorize]
	public class AdminController : Controller
	{
        private readonly IApplicationUserServices _applicationUserServices;
		private readonly IAgentServices _agentServices;
		private readonly IProductServicesRealEstate _productServicesRealEstate;
        private readonly IReservationServices _reservationServices;

        public AdminController(IApplicationUserServices applicationUserServices, IAgentServices agentServices, IProductServicesRealEstate productServicesRealEstate, IReservationServices reservationServices)
        {
            _applicationUserServices = applicationUserServices;
			_agentServices = agentServices;
			_productServicesRealEstate = productServicesRealEstate;
            _reservationServices = reservationServices;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
		{
            var products = await _productServicesRealEstate.GetAllProducts();
            var users = await _applicationUserServices.GetAllUsers();
            var agents = await _agentServices.GetAllAgents();
            var reservations = await _reservationServices.GetAllReservations();
            var v = new AdminDashboard_action();

            v.Products = products.Data;
            v.Users = users.Data;
            v.Agents = agents.Data;
            v.Reservations = reservations.Data;

			return View(v);
		}
	}
}
