using Property.DTOs.Actions;
using Property.DTOs.Category;
using Property.DTOs.Subcategories.SubcategoryRealEstate;
using Property.Services.CategoryServices;
using Property.Services.OtherServices;
using Property.Services.SubCategoryServicesRealEstate;
using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers.Subcategories
{
	public class SubCategoryRealEstateController : Controller
	{
		private readonly ICategoryServices _categoryServices;
		private readonly ISubCategoryServicesRealEstate _subCategoryServicesRealEstate;
		private readonly IOtherServices _otherServices;

		public SubCategoryRealEstateController(ICategoryServices categoryServices, ISubCategoryServicesRealEstate subCategoryServicesRealEstate, IOtherServices otherServices) 
		{
			_categoryServices = categoryServices;
			_subCategoryServicesRealEstate = subCategoryServicesRealEstate;
			_otherServices = otherServices;
		}

		public async Task<IActionResult> AddSubcategory()
		{
			var v = await _categoryServices.GetCategories();
			return View(v.Data);
		}

		[HttpGet]
		public async Task<IActionResult> GetSubcategory()
		{
			var subcategories = await _subCategoryServicesRealEstate.GetAllSubcategoriesRealEstate();
			return View(subcategories.Data);
		}

		public async Task<IActionResult> UpdateSubcategory(int id)
		{
			var subcategory = await _subCategoryServicesRealEstate.GetSubcategoryRealEstateById(id);
            var categories = await _categoryServices.GetCategories();

			var v =new UpdateSubcategoryRealEstate_action();
			v.Subcategory = subcategory.Data;
			v.Categories = categories.Data;

            return View(v);
		}

		[HttpPost]
		public async Task<IActionResult> SaveAddSubcategory(AddSubcategoryRealEstateDTO newSubcategory)
		{
			await _subCategoryServicesRealEstate.AddSubcategoryRealEstate(newSubcategory);

			return RedirectToAction("GetSubcategory");
		}

		[HttpPost]
		public async Task<IActionResult> SaveUpdateSubcategory(UpdateSubcategoryRealEstateDTO updatedSubcategory)
		{
			await _subCategoryServicesRealEstate.UpdateSubcategoryRealEstate(updatedSubcategory);
			return RedirectToAction("GetSubcategory");
		}

		public async Task<IActionResult> DeleteSubcategory(int id)
		{
			await _subCategoryServicesRealEstate.DeleteSubcategoryRealEstate(id);
			return RedirectToAction("GetSubcategory");
		}
	}
}
