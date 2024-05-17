using Property.DTOs.UserApplication;
using Property.Models;

namespace Property.Services.UserApplicationServices
{
	public interface IApplicationUserServices
	{
		Task<ServiceResponse<List<GetApplicationUserDTO>>> GetAllUsers();
	}
}
