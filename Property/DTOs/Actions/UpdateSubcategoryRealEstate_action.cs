using Property.DTOs.Category;
using Property.DTOs.Subcategories.SubcategoryRealEstate;

namespace Property.DTOs.Actions
{
    public class UpdateSubcategoryRealEstate_action
    {
        public List<GetCategoryDTO> Categories { get; set; }
        public GetSubcategoryRealEstateDTO Subcategory {  get; set; }

        public UpdateSubcategoryRealEstate_action()
        {
            Categories = new List<GetCategoryDTO>();
            Subcategory= new GetSubcategoryRealEstateDTO();
        }
    }
}
