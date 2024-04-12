using Microsoft.AspNetCore.Mvc;
using Property.Services.ProductService.ProductServicesRealEstate;

namespace Property.Controllers
{
    public class MarketPlaceController : Controller
    {
        private readonly IProductServicesRealEstate _productServicesRealEstate;

        public MarketPlaceController(IProductServicesRealEstate productServicesRealEstate)
        {
            _productServicesRealEstate = productServicesRealEstate;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Properties()
        {
            var properties = await _productServicesRealEstate.GetAllProducts();
            if (properties != null)
            {
                return View(properties.Data);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
