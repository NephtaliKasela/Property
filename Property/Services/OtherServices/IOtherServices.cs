using Property.DTOs.Actions;
using Property.DTOs.Product.ProductRealEstate;
using Property.Models;

namespace Property.Services.OtherServices
{
    public interface IOtherServices
    {
        (bool, int) CheckIfInteger(string number);
        Task<(List<GetProductRealEstateDTO>, Search)> Filter(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByCountry(List<GetProductRealEstateDTO> properties, Search modelView);
        Task<(List<GetProductRealEstateDTO>, Search)> FilterByCity(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByCategory(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByPropertyType(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByMinPrice(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByMaxPrice(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByMinBed(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByMinBath(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByMinGarage(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByMinArea(List<GetProductRealEstateDTO> properties, Search modelView);
        List<GetProductRealEstateDTO> FilterByMaxArea(List<GetProductRealEstateDTO> properties, Search modelView);


    }
}
