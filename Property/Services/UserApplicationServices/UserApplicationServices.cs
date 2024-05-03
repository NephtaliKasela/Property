using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property.Data;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.UserApplication;
using Property.Models;
using Property.Services.OtherServices;

namespace Property.Services.UserApplicationServices
{
	public class UserApplicationServices: IUserApplicationServices
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IOtherServices _otherServices;

		public UserApplicationServices(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
		{
			_otherServices = otherServices;
			_context = context;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<List<GetUserApplicationDTO>>> GetAllUsers()
		{
			var users = await _context.Users
									.Include(p => p.Agent)
									.Include(p => p.Reservations)
									.ToListAsync();
			var serviceResponse = new ServiceResponse<List<GetUserApplicationDTO>>()
			{
				Data = users.Select(p => _mapper.Map<GetUserApplicationDTO>(p)).ToList()
			};
			return serviceResponse;
		}
	}
}
