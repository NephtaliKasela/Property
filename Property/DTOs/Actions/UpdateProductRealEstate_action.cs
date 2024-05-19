using Property.DTOs.City;
using Property.DTOs.Country;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.PropertyTypeRealEstate;

namespace Property.DTOs.Actions
{
    public class UpdateProductRealEstate_action
	{
		public GetProductRealEstateDTO Product {  get; set; }
		public List<GetPropertyTypeRealEstateDTO> PropertyTypes { get; set; }
		public List<GetCountryDTO> Countries { get; set; }
		public List<GetCityDTO> Cities { get; set; }

		public UpdateProductRealEstate_action()
		{
			Product = new GetProductRealEstateDTO();
            PropertyTypes = new List<GetPropertyTypeRealEstateDTO>();
			Countries = new List<GetCountryDTO>();
			Cities = new List<GetCityDTO>();
		}
	}
}
