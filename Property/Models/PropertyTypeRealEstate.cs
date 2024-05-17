using Property.Models.Products;

namespace Property.Models
{
    public class PropertyTypeRealEstate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public List<ProductRealEstate>? ProductsRealEstate { get; set; }

    }
}
