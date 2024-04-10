using Property.DTOs.Images;
using Property.DTOs.Product;
using Property.Models;
using System.Web;


namespace Property.Services.ImageServices
{
    public interface IProductImageServicesRealEstate
    {
		Task AddProductImage(AddProductImageRealEstateDTO newProductImages);
    }
}
