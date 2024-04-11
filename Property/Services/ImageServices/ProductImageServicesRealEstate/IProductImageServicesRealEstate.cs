using Property.DTOs.Images;
using Property.DTOs.Product;
using Property.Models;
using Property.Models.Images;
using System.Web;


namespace Property.Services.ImageServices
{
    public interface IProductImageServicesRealEstate
    {
		Task AddProductImage(AddProductImageRealEstateDTO newProductImages);
		Task<ServiceResponse<List<ProductImageRealEstate>>> GetImageByProductId(int productId);

	}
}
