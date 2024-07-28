using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Property.DTOs.Actions;
using Property.DTOs.Role;
using Property.Models;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IApplicationUserServices applicationUserServices, IAgentServices agentServices, IProductServicesRealEstate productServicesRealEstate, IReservationServices reservationServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            _applicationUserServices = applicationUserServices;
			_agentServices = agentServices;
			_productServicesRealEstate = productServicesRealEstate;
            _reservationServices = reservationServices;
        }

        [Authorize(Policy = "AdminRole&ManagerRole")]
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

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var v = await _roleManager.Roles.ToListAsync();
            return View(v);
        }

        [Authorize(Policy = "AdminRole")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var r = new IdentityRole(role);
                    var result = await _roleManager.CreateAsync(r);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Roles");
                    }
                    return View();
                }
            }

            return View();
        }

        [Authorize(Policy = "AdminRole")]
        public async Task<IActionResult> GetUsersRoles(string role)
		{
            if (!string.IsNullOrEmpty(role))
            {
				var v = new GetUserRole();
				v.Users = (await _userManager.GetUsersInRoleAsync(role)).ToList();
				v.Role = role.ToString();

				return View(v);
			}
			return View();
		}

        [Authorize(Policy = "AdminRole")]
        public async Task<IActionResult> GetUsers(string role)
		{
			var v = new GetUserRole();
			v.Users = await _userManager.Users.ToListAsync();
			var usersInRole = await _userManager.GetUsersInRoleAsync(role);

            foreach (var user in usersInRole)
            {
                v.Users.Remove(user);
            }

			if (v.Users != null)
			{
				var r = _roleManager.FindByNameAsync(role);
				if (r != null)
				{
                    v.Role = r.Result.ToString(); 
					return View(v);
				}
			}
			return RedirectToAction("GetUsersRoles");
		}

        [Authorize(Policy ="AdminRole")]
        public async Task<IActionResult> AddUserRole(string userEmail, string role)
        {
            var user = _userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {
                var r = _roleManager.FindByNameAsync(role);
                if (r != null)
                {
                    if (!await _userManager.IsInRoleAsync(user.Result, role))
                    {
                        var result = await _userManager.AddToRoleAsync(user.Result, role);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Roles");
                        }
                        else { return RedirectToAction("Index"); }
                    }
                    else { return RedirectToAction("Roles"); }
                }
            }
            return View();
        }

    }
}
