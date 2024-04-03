using Property.DTOs.Category;
using Property.DTOs.Store;
using Property.Services.CategoryServices;
using Property.Services.StoreServices;
using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers
{
	public class StoreController : Controller
	{
		private readonly IStoreServices _storeServices;

		public StoreController(IStoreServices storeServices)
		{
			_storeServices = storeServices;
		}

		public IActionResult AddStore()
		{
			return View();
		}

		public async Task<IActionResult> GetStore()
		{
			var stores = await _storeServices.GetAllStores();
			return View(stores.Data);
		}

		public async Task<IActionResult> UpdateStore(int id)
		{
			var store = await _storeServices.GetStoreById(id);
			return View(store.Data);
		}

		[HttpPost]
		public IActionResult SaveAddStore(AddStoreDTO newStore)
		{
			_storeServices.AddStore(newStore);

			return RedirectToAction("GetStore");
		}

		[HttpPost]
		public async Task<IActionResult> SaveUpdateStore(UpdateStoreDTO updatedStore)
		{
			await _storeServices.UpdateStore(updatedStore);
			return RedirectToAction("GetStore");
		}

		public async Task<IActionResult> SaveDeleteCategory(int id)
		{
			await _storeServices.DeleteStore(id);
			return RedirectToAction("GetStore");
		}
	}
}
