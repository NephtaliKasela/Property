using Property.Models.Products;

namespace Property.DTOs.PropertyTypeRealEstate
{
    public class AddPropertyTypeRealEstateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
    }
}
