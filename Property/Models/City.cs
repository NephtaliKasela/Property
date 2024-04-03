using Property.Models.Products;

namespace Property.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public Country Country { get; set; }

        // Product FK
        public List<ProductRealEstate>? ProductsRealEstate { get; set; }
    }
}
