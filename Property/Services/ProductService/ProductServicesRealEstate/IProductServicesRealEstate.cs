using Property.DTOs.Product.ProductRealEstate;
using Property.Models;

namespace Property.Services.ProductService.ProductServicesRealEstate
{
    public interface IProductServicesRealEstate
    {
        Task<ServiceResponse<List<GetProductRealEstateDTO>>> GetAllProducts();
        Task<ServiceResponse<GetProductRealEstateDTO>> GetProductById(int id);
        Task<ServiceResponse<List<GetProductRealEstateDTO>>> GetProductByAgentId(int agentId);

		Task<ServiceResponse<List<GetProductRealEstateDTO>>> AddProductRentByDay(AddProductRentByDayRealEstateDTO newProduct);
        Task<ServiceResponse<List<GetProductRealEstateDTO>>> AddProductRentByMonth(AddProductRentByMonthRealEstateDTO newProduct);
        Task<ServiceResponse<List<GetProductRealEstateDTO>>> AddProductSell(AddProductSellRealEstateDTO newProduct);
		Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProductRentByDay(UpdateProductRentByDayRealEstateDTO updatedProduct);
		Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProductRentByMonth(UpdateProductRentByMonthRealEstateDTO updatedProduct);
		Task<ServiceResponse<GetProductRealEstateDTO>> UpdateProductSell(UpdateProductSellRealEstateDTO updatedProduct);
        Task<ServiceResponse<List<GetProductRealEstateDTO>>> DeleteProduct(int id);
    }
}
