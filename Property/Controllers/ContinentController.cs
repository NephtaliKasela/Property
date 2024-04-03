using Property.DTOs.Category;
using Property.DTOs.Continent;
using Property.Services.CategoryServices;
using Property.Services.ContinentServices;
using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers
{
    public class ContinentController : Controller
    {
		private readonly IContinentServices _continentServices;

		public ContinentController(IContinentServices continentServices)
		{
			_continentServices = continentServices;
		}

		public IActionResult AddContinent()
        {
            return View();
        }

		[HttpGet]
        public async Task<IActionResult> GetContinent()
        {
			var continent = await _continentServices.GetAllContinents();
            return View(continent.Data);
        }

        public async Task<IActionResult> UpdateContinent(int id)
        {
            var continent = await _continentServices.GetContinentById(id);
            return View(continent.Data);
        }

		[HttpPost]
		public async Task<IActionResult> SaveAddContinent(AddContinentDTO newContinent)
		{
			await _continentServices.AddContinent(newContinent);

			return RedirectToAction("GetContinent");
		}

		[HttpPost]
		public async Task<IActionResult> SaveUpdateContinent(UpdateContinentDTO updatedContinent)
		{
			await _continentServices.UpdateContinent(updatedContinent);
			return RedirectToAction("GetContinent");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteContinent(int id)
		{
			await _continentServices.DeleteContinent(id);
			return RedirectToAction("GetContinent");
		}
	}
}
