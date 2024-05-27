using Property.DTOs.City;
using Property.DTOs.Country;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.PropertyTypeRealEstate;

namespace Property.DTOs.Actions
{
    public class Properties_action
    {
        public List<GetProductRealEstateDTO> Properties { get; set; }
        public List<GetPropertyTypeRealEstateDTO> PropertyTypes { get; set; }
        public List<GetCountryDTO> Countries { get; set; }
        public List<GetCityDTO> Cities { get; set; }

        public Properties_action()
        {
            Properties = new List<GetProductRealEstateDTO>();
            PropertyTypes = new List<GetPropertyTypeRealEstateDTO>();
            Countries = new List<GetCountryDTO>();
            Cities = new List<GetCityDTO>();
        }
    }
}
