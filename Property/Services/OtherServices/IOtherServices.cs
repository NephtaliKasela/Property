using Property.DTOs.Actions;
using Property.DTOs.Product.ProductRealEstate;
using Property.Models;

namespace Property.Services.OtherServices
{
    public interface IOtherServices
    {
        (bool, int) CheckIfInteger(string number);
        List<GetProductRealEstateDTO> Filter(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByCountry(List<GetProductRealEstateDTO> properties, int countryId);
        List<GetProductRealEstateDTO> FilterByCity(List<GetProductRealEstateDTO> properties, int countryId, int cityId);
        List<GetProductRealEstateDTO> FilterByCategory(List<GetProductRealEstateDTO> properties, string category);


    }
}
