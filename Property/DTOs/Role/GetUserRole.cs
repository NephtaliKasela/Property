using Microsoft.AspNetCore.Identity;
using Property.Models;

namespace Property.DTOs.Role
{
	public class GetUserRole
	{
		public List<ApplicationUser> Users { get; set; }
		public string Role { get; set; }

		public GetUserRole()
		{
			Users = new List<ApplicationUser>();
			Role = string.Empty;
		}
	}
}
