using Property.DTOs.City;
using Property.DTOs.Country;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.Store;
using Property.DTOs.Subcategories.SubcategoryRealEstate;

namespace Property.DTOs.Actions
{
	public class UpdateProductRealEstate_action
	{
		public GetProductRealEstateDTO Product {  get; set; }
		public List<GetSubcategoryRealEstateDTO> Subcategories { get; set; }
		public List<GetStoreDTO> Stores { get; set; }
		public List<GetCountryDTO> Countries { get; set; }
		public List<GetCityDTO> Cities { get; set; }

		public UpdateProductRealEstate_action()
		{
			Product = new GetProductRealEstateDTO();
			Subcategories = new List<GetSubcategoryRealEstateDTO>();
			Stores = new List<GetStoreDTO>();
			Countries = new List<GetCountryDTO>();
			Cities = new List<GetCityDTO>();
		}
	}
}
