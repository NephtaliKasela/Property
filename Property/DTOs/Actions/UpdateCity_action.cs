using Property.DTOs.City;
using Property.DTOs.Country;

namespace Property.DTOs.Actions
{
	public class UpdateCity_action
	{
		public GetCityDTO City { get; set; }
		public List<GetCountryDTO> Countries { get; set; }

		public UpdateCity_action()
		{
			City = new GetCityDTO();
			Countries = new List<GetCountryDTO>();
		}
	}
}
