using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property.Data;
using Property.DTOs.Product.ProductRealEstate;
using Property.DTOs.UserApplication;
using Property.Models;
using Property.Services.OtherServices;

namespace Property.Services.UserApplicationServices
{
	public class ApplicationUserServices: IApplicationUserServices
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IOtherServices _otherServices;

		public ApplicationUserServices(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
		{
			_otherServices = otherServices;
			_context = context;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<List<GetApplicationUserDTO>>> GetAllUsers()
		{
			var users = await _context.Users
									.Include(p => p.Agent)
									.Include(p => p.Reservations)
									.ToListAsync();
			var serviceResponse = new ServiceResponse<List<GetApplicationUserDTO>>()
			{
				Data = users.Select(p => _mapper.Map<GetApplicationUserDTO>(p)).ToList()
			};
			return serviceResponse;
		}
	}
}
