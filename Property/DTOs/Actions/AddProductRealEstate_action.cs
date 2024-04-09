using Property.DTOs.City;
using Property.DTOs.Country;
using Property.DTOs.Subcategories.SubcategoryRealEstate;
using Property.DTOs.TransactionType;

namespace Property.DTOs.Actions
{
	public class AddProductRealEstate_action
	{
		public List<GetSubcategoryRealEstateDTO> Subcategories { get; set; }
		public List<GetCountryDTO> Countries { get; set; }
		public List<GetCityDTO> Cities { get; set; }
		public List<GetTransactionTypeDTO> TransactionTypes { get; set; }

		public AddProductRealEstate_action()
        {
            Subcategories = new List<GetSubcategoryRealEstateDTO>();
			Countries = new List<GetCountryDTO>();
			Cities = new List<GetCityDTO>();
			TransactionTypes = new List<GetTransactionTypeDTO>();
        }
    }
}
