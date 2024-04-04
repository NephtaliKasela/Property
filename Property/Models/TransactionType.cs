using Property.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace Property.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign Keys
        public List<ProductRealEstate>? ProductsRealEstate { get; set; }
    }
}
