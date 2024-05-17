using AutoMapper;
using Property.Data;
using Property.Models;
using Microsoft.EntityFrameworkCore;

namespace Property.Services.OtherServices
{
    public class OtherServices : IOtherServices
    {
        private readonly ApplicationDbContext _context;

        public OtherServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public (bool, int) CheckIfInteger(string number)
        {
            try
            {
                int convNumber = Convert.ToInt32(number);
                return (true, convNumber);
            }
            catch
            {
            }
            return (false, 0);
        }
    }
}