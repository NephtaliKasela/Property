using Property.DTOs.UserApplication;
using Property.Models;

namespace Property.Services.UserApplicationServices
{
	public interface IUserApplicationServices
	{
		Task<ServiceResponse<List<GetApplicationUserDTO>>> GetAllUsers();
	}
}
