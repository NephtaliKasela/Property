using Property.DTOs.Category;
using Property.Services.CategoryServices;
using Property.Services.SubCategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Property.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

		public IActionResult AddCategory()
		{
			return View();
		}

		public async Task<IActionResult> GetCategory()
		{
			var subCategory = await _categoryServices.GetCategories();
			return View(subCategory.Data);
		}

		public async Task<IActionResult> UpdateCategory(int id)
		{
			var c = await _categoryServices.GetCategoryById(id);
			return View(c.Data);
		}

		[HttpPost]
        public IActionResult SaveAddCategory(AddCategoryDTO newCategory)
        {
            _categoryServices.AddCategory(newCategory);

            return RedirectToAction("GetCategory");
        }

        [HttpPost]
        public async Task<IActionResult> SaveUpdateCategory(UpdateCategoryDTO updatedSubCategory)
        {
            var category = await _categoryServices.UpdateCategory(updatedSubCategory);
            return RedirectToAction("GetCategory");
        }

        public IActionResult DeleteSubCategory()
        {
            return View();
        }

        public async Task<IActionResult> SaveDeleteCategory(int id)
        {
			var subCategory = await _categoryServices.DeleteCategory(id);
			return RedirectToAction("GetCategory");
		}
    }
}
