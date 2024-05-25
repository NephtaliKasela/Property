using Property.DTOs.Actions;
using Property.Services.OtherServices;
using Property.Services.SubCategoryServicesRealEstate;
using Microsoft.AspNetCore.Mvc;
using Property.DTOs.PropertyTypeRealEstate;
using Microsoft.AspNetCore.Authorization;

namespace Property.Controllers
{
    [Authorize]
    public class PropertyTypeRealEstateController : Controller
    {
        private readonly IPropertyTypeServicesRealEstate _propertyTypeServicesRealEstate;

        public PropertyTypeRealEstateController(IPropertyTypeServicesRealEstate propertyTypeServicesRealEstate)
        {
            _propertyTypeServicesRealEstate = propertyTypeServicesRealEstate;
        }

        [HttpGet]
        public IActionResult AddPropertyType()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPropertyTypes()
        {
            var propertyTypes = await _propertyTypeServicesRealEstate.GetAllPropertyTypesRealEstate();
            return View(propertyTypes.Data);
        }

		[HttpGet]
		public async Task<IActionResult> UpdatePropertyType(int id)
        {
            var propertyType = await _propertyTypeServicesRealEstate.GetPropertyTypeRealEstateById(id);

            return View(propertyType.Data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAddPropertyType(AddPropertyTypeRealEstateDTO newPropertyType)
        {
            await _propertyTypeServicesRealEstate.AddPropertyTypeRealEstate(newPropertyType);

            return RedirectToAction("GetPropertyTypes");
        }

        [HttpPost]
        public async Task<IActionResult> SaveUpdatePropertyType(UpdatePropertyTypeRealEstateDTO updatedSubcategory)
        {
            await _propertyTypeServicesRealEstate.UpdatePropertyTypeRealEstate(updatedSubcategory);
            return RedirectToAction("GetPropertyTypes");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            await _propertyTypeServicesRealEstate.DeletePropertyTypeRealEstate(id);
            return RedirectToAction("GetPropertyTypes");
        }
    }
}
