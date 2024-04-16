using Property.Models.Products;

namespace Property.Models
{
    public class SellRealEstate
    {
        public int Id { get; set; }
        public int ProductRealEstateId  { get; set; }
        public ProductRealEstate ProductRealEstate  { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int Days { get; set; }
    }
}
