using Property.DTOs.Actions;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.Subcategories.SubcategoryRealEstate;
using Property.Services.CityServices;
using Property.Services.CountryServices;
using Property.Services.ProductService;
using Property.Services.ProductService.ProductServicesRealEstate;
using Property.Services.StoreServices;
using Property.Services.SubCategoryServices;
using Property.Services.SubCategoryServicesRealEstate;
using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers.Products
{
    public class ProductRealEstateController : Controller
    {
        private readonly IStoreServices _storeServices;
		private readonly ICountryServices _countryServices;
		private readonly ICityServices _cityServices;
		private readonly IProductServicesRealEstate _productServicesRealEstate;
		private readonly ISubCategoryServicesRealEstate _subCategoryServicesRealEstate;

		public ProductRealEstateController(IProductServicesRealEstate productServicesRealEstate, ISubCategoryServicesRealEstate subCategoryServicesRealEstate, IStoreServices storeServices, ICountryServices countryServices, ICityServices cityServices)
        {
            _productServicesRealEstate = productServicesRealEstate;
			_subCategoryServicesRealEstate = subCategoryServicesRealEstate;
            _storeServices = storeServices;
			_countryServices = countryServices;
			_cityServices = cityServices;
		}

        public async Task<IActionResult> GetProduct()
        {
            var products = await _productServicesRealEstate.GetAllProducts();
			return View(products.Data);
        }

        public async Task<IActionResult> AddProduct()
        {
            var subcategories = await _subCategoryServicesRealEstate.GetAllSubcategoriesRealEstate();
            var stores = await _storeServices.GetAllStores();
            var countries = await _countryServices.GetAllCountries();
            var cities = await _cityServices.GetAllCities();

            var v = new AddProductRealEstate_action();

            v.Subcategories = subcategories.Data;
            v.Stores = stores.Data;
            v.Countries = countries.Data;
            v.Cities = cities.Data;

            return View(v);
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productServicesRealEstate.GetProductById(id);

            var v = new UpdateProductRealEstate_action();

            var subcategories = await _subCategoryServicesRealEstate.GetAllSubcategoriesRealEstate();
            var stores = await _storeServices.GetAllStores();
            var countries = await _countryServices.GetAllCountries();
            var cities = await _cityServices.GetAllCities();

            v.Subcategories = subcategories.Data;
            v.Stores = stores.Data;
            v.Countries = countries.Data;
            v.Cities = cities.Data;
            v.Product = product.Data;

            return View(v);
        }

		[HttpPost]
		public async Task<IActionResult> SaveAddProduct(AddProductRealEstateDTO newProduct)
		{
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
