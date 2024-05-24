using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property.DTOs.Images;
using Property.Services.ImageServices;

namespace Property.Controllers.Images
{
	[Authorize]
	public class ProductImageRealEstateController : Controller
	{
		private readonly IProductImageServicesRealEstate _productImageServices;

		public ProductImageRealEstateController(IProductImageServicesRealEstate productImageServices)
		{ 
			_productImageServices = productImageServices;
		}

		[HttpPost]
		public async Task<IActionResult> AddProductImage(AddProductImageRealEstateDTO newProductImage)
		{
			await _productImageServices.AddProductImage(newProductImage);
			return RedirectToAction("Dashboard", "Agent");
		}
	}
}
