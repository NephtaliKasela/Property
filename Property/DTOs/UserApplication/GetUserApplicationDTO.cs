using Property.Models;

namespace Property.DTOs.UserApplication
{
	public class GetUserApplicationDTO
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public Models.Agent? Agent { get; set; }
		public List<Models.Reservation> Reservations { get; set; }

	}
}
