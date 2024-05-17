using Property.Models.Products;

namespace Property.DTOs.PropertyTypeRealEstate
{
    public class GetPropertyTypeRealEstateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public List<ProductRealEstate>? ProductsRealEstate { get; set; }
    }
}
