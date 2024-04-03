using Property.DTOs.Category;
using Property.DTOs.Product;
using Property.DTOs.Product.ProductRealEstate;

namespace Property.DTOs.Actions
{
    public class Home_action
    { 
        public List<GetProductRealEstateDTO> ProductsRealEstate { get; set; }  
        public List<GetCategoryDTO> Categories { get; set; }  

        public Home_action()
        {
            Categories = new List<GetCategoryDTO>();
            ProductsRealEstate = new List<GetProductRealEstateDTO>();
        }

    }
}
