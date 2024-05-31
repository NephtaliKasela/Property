using Microsoft.AspNetCore.Mvc;
using Property.DTOs.Actions;
using Property.Services.CityServices;
using Property.Services.CountryServices;
using Property.Services.OtherServices;
using Property.Services.ProductService.ProductServicesRealEstate;
using Property.Services.SubCategoryServicesRealEstate;

namespace Property.Controllers
{
    public class MarketPlaceController : Controller
    {
        private readonly IProductServicesRealEstate _productServicesRealEstate;
        private readonly IPropertyTypeServicesRealEstate _propertyTypeServicesRealEstate;
        private readonly ICountryServices _countryServices;
        private readonly ICityServices _cityServices;
        private readonly IOtherServices _otherServices;

        public MarketPlaceController(IProductServicesRealEstate productServicesRealEstate, IPropertyTypeServicesRealEstate propertyTypeServicesRealEstate,
                                        ICountryServices countryServices, ICityServices cityServices, IOtherServices otherServices)
        {
            _productServicesRealEstate = productServicesRealEstate;
            _propertyTypeServicesRealEstate = propertyTypeServicesRealEstate;
            _countryServices = countryServices;
            _cityServices = cityServices;
            _otherServices = otherServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Properties()
        {
            var properties = await _productServicesRealEstate.GetAllProducts();
            if (properties.Data != null)
            {
                var countries = await _countryServices.GetAllCountries();
                var cities = await _cityServices.GetAllCities();
                var propertyTyes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();

                var v = new Properties_action();
                v.Properties = properties.Data;
                v.Countries = countries.Data;
                v.Cities = cities.Data;
                v.PropertyTypes = propertyTyes.Data;

                return View(v);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Search(Search modelView)
        {
            var properties = await _productServicesRealEstate.GetAllProducts();
            if (properties.Data != null)
            {
                var countries = await _countryServices.GetAllCountries();
                var cities = await _cityServices.GetAllCities();
                var propertyTyes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();

                var v = new Properties_action();
                v.Properties = properties.Data;
                v.Countries = countries.Data;
                v.Cities = cities.Data;
                v.PropertyTypes = propertyTyes.Data;
                v.Search = modelView;

                v.Properties = _otherServices.Filter(properties.Data, modelView);

                return View(v);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Icons()
        {
            return View();
        }
    }
}
