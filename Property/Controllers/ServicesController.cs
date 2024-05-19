using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
