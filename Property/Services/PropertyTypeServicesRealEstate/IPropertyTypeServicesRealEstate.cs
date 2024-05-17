using Property.DTOs.PropertyTypeRealEstate;
using Property.Models;

namespace Property.Services.SubCategoryServicesRealEstate
{
    public interface IPropertyTypeServicesRealEstate
	{
		Task<ServiceResponse<List<GetPropertyTypeRealEstateDTO>>> GetAllPropertyTypesRealEstate();
		Task<ServiceResponse<GetPropertyTypeRealEstateDTO>> GetPropertyTypeRealEstateById(int id);
		Task<ServiceResponse<List<GetPropertyTypeRealEstateDTO>>> AddPropertyTypeRealEstate(AddPropertyTypeRealEstateDTO newSubcategory);
		Task<ServiceResponse<GetPropertyTypeRealEstateDTO>> UpdatePropertyTypeRealEstate(UpdatePropertyTypeRealEstateDTO updatedSubcategory);
		Task<ServiceResponse<List<GetPropertyTypeRealEstateDTO>>> DeletePropertyTypeRealEstate(int id);
	}
}
