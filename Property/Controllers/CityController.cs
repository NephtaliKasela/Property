using Property.DTOs.Actions;
using Property.DTOs.City;
using Property.DTOs.Country;
using Property.Services.CityServices;
using Property.Services.ContinentServices;
using Property.Services.CountryServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Property.Controllers
{
	[Authorize]
	public class CityController : Controller
	{
		private readonly ICountryServices _countryServices;
		private readonly ICityServices _cityServices;

		public CityController(ICountryServices countryServices, ICityServices cityServices)
		{
			_countryServices = countryServices;
			_cityServices = cityServices;
		}

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
		public async Task<IActionResult> AddCity()
		{
			var countries = await _countryServices.GetAllCountries();
			return View(countries.Data);
		}

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
		public async Task<IActionResult> GetCity()
		{
			var cities = await _cityServices.GetAllCities();
			return View(cities.Data);
		}

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
		public async Task<IActionResult> UpdateCity(int id)
		{
			var city = await _cityServices.GetCityById(id);
			var countries = await _countryServices.GetAllCountries();

			var v = new UpdateCity_action();
			v.City = city.Data;
			v.Countries = countries.Data;

			return View(v);
		}

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
		public async Task<IActionResult> SaveAddCity(AddCityDTO newCity)
		{
			await _cityServices.AddCity(newCity);

			return RedirectToAction("GetCity");
		}

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
		public async Task<IActionResult> SaveUpdateCity(UpdateCityDTO updatedCity)
		{
			await _cityServices.UpdateCity(updatedCity);
			return RedirectToAction("GetCity");
		}

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
		public async Task<IActionResult> DeleteCity(int id)
		{
			await _cityServices.DeleteCity(id);
			return RedirectToAction("GetCity");
		}
	}
}
