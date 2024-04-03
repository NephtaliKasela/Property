using Property.DTOs.City;
using Property.DTOs.Country;
using Property.Models;

namespace Property.Services.CityServices
{
	public interface ICityServices
	{
		Task<ServiceResponse<List<GetCityDTO>>> GetAllCities();
		Task<ServiceResponse<GetCityDTO>> GetCityById(int id);
		Task<ServiceResponse<List<GetCityDTO>>> AddCity(AddCityDTO newCity);
		Task<ServiceResponse<GetCityDTO>> UpdateCity(UpdateCityDTO updatedCity);
		Task<ServiceResponse<List<GetCityDTO>>> DeleteCity(int id);
	}
}
