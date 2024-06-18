using Property.DTOs.Continent;
using Property.Services.ContinentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Property.Controllers
{
	[Authorize]
    public class ContinentController : Controller
    {
		private readonly IContinentServices _continentServices;

		public ContinentController(IContinentServices continentServices)
		{
			_continentServices = continentServices;
		}

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
		public IActionResult AddContinent()
        {
            return View();
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        public async Task<IActionResult> GetContinent()
        {
			var continent = await _continentServices.GetAllContinents();
            return View(continent.Data);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
		public async Task<IActionResult> UpdateContinent(int id)
        {
            var continent = await _continentServices.GetContinentById(id);
            return View(continent.Data);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
		public async Task<IActionResult> SaveAddContinent(AddContinentDTO newContinent)
		{
			await _continentServices.AddContinent(newContinent);

			return RedirectToAction("GetContinent");
		}

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
		public async Task<IActionResult> SaveUpdateContinent(UpdateContinentDTO updatedContinent)
		{
			await _continentServices.UpdateContinent(updatedContinent);
			return RedirectToAction("GetContinent");
		}

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
		public async Task<IActionResult> DeleteContinent(int id)
		{
			await _continentServices.DeleteContinent(id);
			return RedirectToAction("GetContinent");
		}
	}
}
