using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers
{
    public class MarketPlaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Properties()
        {
            return View();
        }
    }
}
