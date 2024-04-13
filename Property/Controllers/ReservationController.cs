using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
