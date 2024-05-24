﻿using Property.DTOs.Actions;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.PropertyTypeRealEstate;
using Property.Services.CityServices;
using Property.Services.CountryServices;
using Property.Services.ProductService.ProductServicesRealEstate;
using Property.Services.SubCategoryServicesRealEstate;
using Microsoft.AspNetCore.Mvc;
using Property.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Property.Services.AgentServices;
using Property.Services.UserApplicationServices;
using Property.DTOs.UserApplication;

namespace Property.Controllers.Products
{
    [Authorize]
    public class ProductRealEstateController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IApplicationUserServices _applicationUserServices;
		private readonly IAgentServices _agentServices;
		private readonly ICountryServices _countryServices;
		private readonly ICityServices _cityServices;
		private readonly IProductServicesRealEstate _productServicesRealEstate;
		private readonly IPropertyTypeServicesRealEstate _propertyTypeServicesRealEstate;

        public ProductRealEstateController(UserManager<ApplicationUser> userManager, IApplicationUserServices applicationUserServices, IAgentServices agentServices, IProductServicesRealEstate productServicesRealEstate, IPropertyTypeServicesRealEstate propertyTypeServicesRealEstate, ICountryServices countryServices, ICityServices cityServices)
        {
            _userManager = userManager;
			_applicationUserServices = applicationUserServices;
			_agentServices = agentServices;
			_productServicesRealEstate = productServicesRealEstate;
			_propertyTypeServicesRealEstate = propertyTypeServicesRealEstate;
			_countryServices = countryServices;
			_cityServices = cityServices;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> ProductDetails(int id)
		{
			var product = await _productServicesRealEstate.GetProductById(id);
			if(product.Data != null)
			{
                return View(product.Data);
            }
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> GetProduct()
        {
            var products = await _productServicesRealEstate.GetAllProducts();
			return View(products.Data);
        }

        public async Task<IActionResult> Choice()
        {
			//Get the current user
			ApplicationUser currentUser = await _userManager.GetUserAsync(User);
			var user = await _applicationUserServices.GetUserById(currentUser.Id);

			if (user.Data != null && user.Data.Agent != null)
            {
			    return View();	
			}
			return RedirectToAction("Index", "Home");
				
        }

        public async Task<IActionResult> AddProductSell()
        {
			//Get the current user
			ApplicationUser currentUser = await _userManager.GetUserAsync(User);
			var user = await _applicationUserServices.GetUserById(currentUser.Id);

			if (user.Data != null && user.Data.Agent != null)
			{
				var propertyTypes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();
				var countries = await _countryServices.GetAllCountries();
				var cities = await _cityServices.GetAllCities();

				var v = new AddProductRealEstate_action();

				v.PropertyTypes = propertyTypes.Data;
				v.Countries = countries.Data;
				v.Cities = cities.Data;

				return View(v);
			}
			return RedirectToAction("Index", "Home");

		}

        public async Task<IActionResult> AddProductRentByDay()
        {
			//Get the current user
			ApplicationUser currentUser = await _userManager.GetUserAsync(User);
			var user = await _applicationUserServices.GetUserById(currentUser.Id);

			if (user.Data != null && user.Data.Agent != null)
			{
				var propertyTypes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();
				var countries = await _countryServices.GetAllCountries();
				var cities = await _cityServices.GetAllCities();

				var v = new AddProductRealEstate_action();

				v.PropertyTypes = propertyTypes.Data;
				v.Countries = countries.Data;
				v.Cities = cities.Data;

				return View(v);
			}
			return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddProductRentByMonth()
        {
			//Get the current user
			ApplicationUser currentUser = await _userManager.GetUserAsync(User);
			var user = await _applicationUserServices.GetUserById(currentUser.Id);

			if (user.Data != null && user.Data.Agent != null)
			{
				var propertyTypes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();
				var countries = await _countryServices.GetAllCountries();
				var cities = await _cityServices.GetAllCities();

				var v = new AddProductRealEstate_action();

				v.PropertyTypes = propertyTypes.Data;
				v.Countries = countries.Data;
				v.Cities = cities.Data;

				return View(v);
			}
			return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productServicesRealEstate.GetProductById(id);

            if(product.Data.Rent != null && product.Data.Rent.RentRealEstatePerDay != null)
            {
                return RedirectToAction("UpdateProductRentByDay", new { id = id });
            }
			else if (product.Data.Rent != null && product.Data.Rent.RentRealEstatePerMounth != null)
			{
				return RedirectToAction("UpdateProductRentByMonth", new { id = id });
			}
			else if (product.Data.Sell != null)
			{
                return RedirectToAction("UpdateProductSell", new { id = id });
			}

			return RedirectToAction("Dashboard", "Agent");
		}

        public async Task<IActionResult> UpdateProductSell(int id)
        {
            var product = await _productServicesRealEstate.GetProductById(id);

            var v = new UpdateProductRealEstate_action();

            var propertyTypes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();
            var countries = await _countryServices.GetAllCountries();
            var cities = await _cityServices.GetAllCities();

            v.PropertyTypes = propertyTypes.Data;
            v.Countries = countries.Data;
            v.Cities = cities.Data;
            v.Product = product.Data;

            return View(v);
        }

        public async Task<IActionResult> UpdateProductRentByDay(int id)
        {
            var product = await _productServicesRealEstate.GetProductById(id);

            var v = new UpdateProductRealEstate_action();

            var propertyTypes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();
            var countries = await _countryServices.GetAllCountries();
            var cities = await _cityServices.GetAllCities();

            v.PropertyTypes = propertyTypes.Data;
            v.Countries = countries.Data;
            v.Cities = cities.Data;
            v.Product = product.Data;

            return View(v);
        }

		public async Task<IActionResult> UpdateProductRentByMonth(int id)
		{
			var product = await _productServicesRealEstate.GetProductById(id);

			var v = new UpdateProductRealEstate_action();

			var propertyTypes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();
			var countries = await _countryServices.GetAllCountries();
			var cities = await _cityServices.GetAllCities();

			v.PropertyTypes = propertyTypes.Data;
			v.Countries = countries.Data;
			v.Cities = cities.Data;
			v.Product = product.Data;

			return View(v);
		}

        [HttpPost]
		public async Task<IActionResult> SaveAddProductSell(AddProductSellRealEstateDTO newProduct)
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				newProduct.User = user;
			}

			await _productServicesRealEstate.AddProductSell(newProduct);

			return RedirectToAction("Dashboard", "Agent");
		}

		[HttpPost]
		public async Task<IActionResult> SaveAddProductRentByDay(AddProductRentByDayRealEstateDTO newProduct)
		{
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
				newProduct.User = user;
            }

            await _productServicesRealEstate.AddProductRentByDay(newProduct);

			return RedirectToAction("Dashboard", "Agent");
		}

		[HttpPost]
		public async Task<IActionResult> SaveAddProductRentByMonth(AddProductRentByMonthRealEstateDTO newProduct)
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				newProduct.User = user;
			}

			await _productServicesRealEstate.AddProductRentByMonth(newProduct);

			return RedirectToAction("Dashboard", "Agent");
		}


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveUpdateProductSell(UpdateProductSellRealEstateDTO updatedProduct)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                updatedProduct.User = user;
            }
            await _productServicesRealEstate.UpdateProductSell(updatedProduct);
            return RedirectToAction("Dashboard", "Agent");
        }

        [Authorize]
		[HttpPost]
		public async Task<IActionResult> SaveUpdateProductRentByDay(UpdateProductRentByDayRealEstateDTO updatedProduct)
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				updatedProduct.User = user;
			}
			await _productServicesRealEstate.UpdateProductRentByDay(updatedProduct);
			return RedirectToAction("Dashboard", "Agent");
		}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveUpdateProductRentByMonth(UpdateProductRentByMonthRealEstateDTO updatedProduct)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                updatedProduct.User = user;
            }
            await _productServicesRealEstate.UpdateProductRentByMonth(updatedProduct);
            return RedirectToAction("Dashboard", "Agent");
        }

        public async Task<IActionResult> DeleteProduct(int id)
		{
			await _productServicesRealEstate.DeleteProduct(id);
			return RedirectToAction("GetProduct");
		}
	}
}
