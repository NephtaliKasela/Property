using Property.DTOs.Continent;
using Property.DTOs.Country;

namespace Property.DTOs.Actions
{
	public class UpdateCountry_action
	{
		public GetCountryDTO Country { get; set; }
		public List<GetContinentDTO> Continents { get; set; }
	}
}
