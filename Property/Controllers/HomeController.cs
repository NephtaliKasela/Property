using Microsoft.AspNetCore.Mvc;
using Property.Models;
using Property.Services.ProductService.ProductServicesRealEstate;
using System.Diagnostics;

namespace Property.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServicesRealEstate _productServicesRealEstate;

        public HomeController(ILogger<HomeController> logger, IProductServicesRealEstate productServicesRealEstate)
        {
            _logger = logger;
            _productServicesRealEstate = productServicesRealEstate;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productServicesRealEstate.GetAllProducts();
            return View(products.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
