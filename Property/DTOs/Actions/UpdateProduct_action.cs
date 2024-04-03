using Property.DTOs.Store;
using Property.DTOs.Product;

namespace Property.DTOs.Actions
{
    public class UpdateProduct_action
    {
        public List<GetStoreDTO> Stores { get; set; }

        public UpdateProduct_action() 
        { 
            Stores = new List<GetStoreDTO>();
        }
    }
}
