using Property.DTOs.City;
using Property.DTOs.Country;
using Property.DTOs.PropertyTypeRealEstate;

namespace Property.DTOs.Actions
{
    public class AddProductRealEstate_action
	{
		public List<GetPropertyTypeRealEstateDTO> PropertyTypes { get; set; }
		public List<GetCountryDTO> Countries { get; set; }
		public List<GetCityDTO> Cities { get; set; }

		public AddProductRealEstate_action()
        {
			PropertyTypes = new List<GetPropertyTypeRealEstateDTO>(); 
			Countries = new List<GetCountryDTO>();
			Cities = new List<GetCityDTO>();
        }
    }
}
