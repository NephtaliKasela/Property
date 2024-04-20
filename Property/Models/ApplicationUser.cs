using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public int AgentId { get; set; }
        public Agent? Agent { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
