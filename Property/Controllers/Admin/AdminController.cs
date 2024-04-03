using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers.Admin
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
