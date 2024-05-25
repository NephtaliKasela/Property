using Property.DTOs.Actions;
using Property.DTOs.Continent;
using Property.DTOs.Country;
using Property.Services.ContinentServices;
using Property.Services.CountryServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Property.Controllers
{
	[Authorize]
	public class CountryController : Controller
	{
		private readonly IContinentServices _continentServices;
		private readonly ICountryServices _countryServices;

		public CountryController(IContinentServices continentServices, ICountryServices countryServices)
		{
			_continentServices = continentServices;
			_countryServices = countryServices;
		}

		[HttpGet]
		public async Task<IActionResult> AddCountry()
		{
			var continents = await _continentServices.GetAllContinents();
			return View(continents.Data);
		}

		[HttpGet]
		public async Task<IActionResult> GetCountry()
		{
			var countries = await _countryServices.GetAllCountries();
			return View(countries.Data);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateCountry(int id)
		{
			var country = await _countryServices.GetCountryById(id);
			var continents = await _continentServices.GetAllContinents();

			var v = new UpdateCountry_action();
			v.Country = country.Data;
			v.Continents = continents.Data;

			return View(v);
		}

		[HttpPost]
		public async Task<IActionResult> SaveAddCountry(AddCountryDTO newCountry)
		{
			await _countryServices.AddCountry(newCountry);

			return RedirectToAction("GetCountry");
		}

		[HttpPost]
		public async Task<IActionResult> SaveUpdateCountry(UpdateCountryDTO updatedCountry)
		{
			await _countryServices.UpdateCountry(updatedCountry);
			return RedirectToAction("GetCountry");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteCountry(int id)
		{
			await _countryServices.DeleteCountry(id);
			return RedirectToAction("GetCountry");
		}
	}
}
