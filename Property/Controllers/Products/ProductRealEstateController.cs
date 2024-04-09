using Property.DTOs.Actions;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.Subcategories.SubcategoryRealEstate;
using Property.Services.CityServices;
using Property.Services.CountryServices;
using Property.Services.ProductService.ProductServicesRealEstate;
using Property.Services.SubCategoryServicesRealEstate;
using Microsoft.AspNetCore.Mvc;
using Property.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Property.Services.TransactionTypeServices;

namespace Property.Controllers.Products
{
    public class ProductRealEstateController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICountryServices _countryServices;
		private readonly ICityServices _cityServices;
		private readonly IProductServicesRealEstate _productServicesRealEstate;
		private readonly ISubCategoryServicesRealEstate _subCategoryServicesRealEstate;
		private readonly ITransactionTypeServices _transactionTypeServices;

		public ProductRealEstateController(UserManager<ApplicationUser> userManager, IProductServicesRealEstate productServicesRealEstate, ISubCategoryServicesRealEstate subCategoryServicesRealEstate, ICountryServices countryServices, ICityServices cityServices, ITransactionTypeServices transactionTypeServices)
        {
            _userManager = userManager;
            _productServicesRealEstate = productServicesRealEstate;
			_subCategoryServicesRealEstate = subCategoryServicesRealEstate;
			_countryServices = countryServices;
			_cityServices = cityServices;
            _transactionTypeServices = transactionTypeServices;
		}

        public async Task<IActionResult> GetProduct()
        {
            var products = await _productServicesRealEstate.GetAllProducts();
			return View(products.Data);
        }

        public async Task<IActionResult> AddProduct()
        {
            var subcategories = await _subCategoryServicesRealEstate.GetAllSubcategoriesRealEstate();
            var countries = await _countryServices.GetAllCountries();
            var cities = await _cityServices.GetAllCities();
            var transactionTypes = await _transactionTypeServices.GetAllTransactionTypes();

			var v = new AddProductRealEstate_action();

            v.Subcategories = subcategories.Data;
            v.Countries = countries.Data;
            v.Cities = cities.Data;
            v.TransactionTypes = transactionTypes.Data;

            return View(v);
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productServicesRealEstate.GetProductById(id);

            var v = new UpdateProductRealEstate_action();

            var subcategories = await _subCategoryServicesRealEstate.GetAllSubcategoriesRealEstate();
            var countries = await _countryServices.GetAllCountries();
            var cities = await _cityServices.GetAllCities();
			var transactionTypes = await _transactionTypeServices.GetAllTransactionTypes();

			v.Subcategories = subcategories.Data;
            v.Countries = countries.Data;
            v.Cities = cities.Data;
            v.Product = product.Data;
			v.TransactionTypes = transactionTypes.Data;

			return View(v);
        }

		[HttpPost]
		public async Task<IActionResult> SaveAddProduct(AddProductRealEstateDTO newProduct)
		{
            ApplicationUser user = await _userManager.GetUserAsync(User);
            //if (user != null && user.Agent != null)
            //{
            //    newProduct.Agent = user.Agent;
            //}

			await _productServicesRealEstate.AddProduct(newProduct);

			return RedirectToAction("GetProduct");
		}

		[HttpPost]
		public async Task<IActionResult> SaveUpdateProduct(UpdateProductRealEsteDTO updatedProduct)
		{
			await _productServicesRealEstate.UpdateProduct(updatedProduct);
			return RedirectToAction("GetProduct");
		}

		public async Task<IActionResult> DeleteProduct(int id)
		{
			await _productServicesRealEstate.DeleteProduct(id);
			return RedirectToAction("GetProduct");
		}
	}
}
