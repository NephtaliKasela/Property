using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
