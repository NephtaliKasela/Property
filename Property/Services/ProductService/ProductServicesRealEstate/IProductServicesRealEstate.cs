using Property.DTOs.Product.ProductRealEstate;
using Property.Models;

namespace Property.Services.ProductService.ProductServicesRealEstate
{
    public interface IProductServicesRealEstate
    {
        Task<ServiceResponse<List<GetProductRealEstateDTO>>> GetAllProducts();
        Task<ServiceResponse<GetProductRealEstateDTO>> GetProductById(int id);
        Task<ServiceResponse<List<GetProductRealEstateDTO>>> AddProduct(AddProductRealEstateDTO newProduct);
        Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProduct(UpdateProductRealEsteDTO updatedProduct);
        Task<ServiceResponse<List<GetProductRealEstateDTO>>> DeleteProduct(int id);
    }
}
