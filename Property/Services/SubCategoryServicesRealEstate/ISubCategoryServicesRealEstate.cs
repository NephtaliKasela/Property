using Property.DTOs.Category;
using Property.DTOs.Subcategories.SubcategoryRealEstate;
using Property.Models;

namespace Property.Services.SubCategoryServicesRealEstate
{
	public interface ISubCategoryServicesRealEstate
	{
		Task<ServiceResponse<List<GetSubcategoryRealEstateDTO>>> GetAllSubcategoriesRealEstate();
		Task<ServiceResponse<GetSubcategoryRealEstateDTO>> GetSubcategoryRealEstateById(int id);
		//Task<ServiceResponse<List<GetSubcategoryRealEstateDTO>>> AddSubcategoryRealEstate(AddSubcategoryRealEstateDTO newSubcategory, GetCategoryDTO category);
		Task<ServiceResponse<List<GetSubcategoryRealEstateDTO>>> AddSubcategoryRealEstate(AddSubcategoryRealEstateDTO newSubcategory);
		Task<ServiceResponse<GetSubcategoryRealEstateDTO>> UpdateSubcategoryRealEstate(UpdateSubcategoryRealEstateDTO updatedSubcategory);
		Task<ServiceResponse<List<GetSubcategoryRealEstateDTO>>> DeleteSubcategoryRealEstate(int id);
	}
}
