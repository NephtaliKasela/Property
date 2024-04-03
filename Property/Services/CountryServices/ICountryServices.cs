using Property.DTOs.Country;
using Property.Models;

namespace Property.Services.CountryServices
{
	public interface ICountryServices
	{
		Task<ServiceResponse<List<GetCountryDTO>>> GetAllCountries();
		Task<ServiceResponse<GetCountryDTO>> GetCountryById(int id);
		Task<ServiceResponse<List<GetCountryDTO>>> AddCountry(AddCountryDTO newCountry);
		Task<ServiceResponse<GetCountryDTO>> UpdateCountry(UpdateCountryDTO updatedCountry);
		Task<ServiceResponse<List<GetCountryDTO>>> DeleteCountry(int id);
	}
}
