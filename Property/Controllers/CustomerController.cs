using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Property.Models;
using Property.Services.UserApplicationServices;

namespace Property.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IApplicationUserServices _userApplication;
        public CustomerController(IApplicationUserServices userApplication)
        {
            _userApplication = userApplication;
        }

		[HttpGet]
		public async Task<IActionResult> Index()
        {
            var v = await _userApplication.GetAllUsers(); 
            if(v.Data is not null)
            {
				return View(v.Data);
			}
			return RedirectToAction("Index", "Admin");
		}
    }
}
