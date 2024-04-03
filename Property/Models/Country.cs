using Property.Models.Products;

namespace Property.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Freign Keys
        public Continent Continent { get; set; }
        public List<City>? Cities { get; set; }

        // Product FK
        public List<ProductRealEstate>? ProductsRealEstate {  get; set; }    
    }
}
