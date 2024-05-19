using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
