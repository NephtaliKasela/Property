using Property.Models.Products;

namespace Property.Models.Images
{
    public class ProductImageRealEstate
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        public ProductRealEstate ProductRealEstate { get; set; }
    }
}
