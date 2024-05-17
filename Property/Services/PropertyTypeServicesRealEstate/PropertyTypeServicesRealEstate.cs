using AutoMapper;
using Property.Data;
using Property.Models;
using Property.Services.OtherServices;
using Property.Services.SubCategoryServicesRealEstate;
using Microsoft.EntityFrameworkCore;
using Property.DTOs.PropertyTypeRealEstate;

namespace Property.Services.SubCategoryServices
{
    public class PropertyTypeServicesRealEstate : IPropertyTypeServicesRealEstate
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IOtherServices _otherServices;

		public PropertyTypeServicesRealEstate(ApplicationDbContext context, IMapper mapper, IOtherServices otherServices)
		{
			_context = context;
			_mapper = mapper;
			_otherServices = otherServices;
		}

		public async Task<ServiceResponse<List<GetPropertyTypeRealEstateDTO>>> AddPropertyTypeRealEstate(AddPropertyTypeRealEstateDTO newPropertyType)
		{
			var serviceResponse = new ServiceResponse<List<GetPropertyTypeRealEstateDTO>>();
			var propertyType = _mapper.Map<PropertyTypeRealEstate>(newPropertyType);

			await _context.PropertyTypeRealEstate.AddAsync(propertyType);
			await _context.SaveChangesAsync();

			serviceResponse.Data = await _context.PropertyTypeRealEstate
				.Select(p => _mapper.Map<GetPropertyTypeRealEstateDTO>(p)).ToListAsync();

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetPropertyTypeRealEstateDTO>>> GetAllPropertyTypesRealEstate()
		{
			var propertyTypes = await _context.PropertyTypeRealEstate
				.Include(x => x.ProductsRealEstate)
				.ToListAsync();
			var serviceResponse = new ServiceResponse<List<GetPropertyTypeRealEstateDTO>>()
			{
				Data = propertyTypes.Select(p => _mapper.Map<GetPropertyTypeRealEstateDTO>(p)).ToList()
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetPropertyTypeRealEstateDTO>> GetPropertyTypeRealEstateById(int id)
		{
			var propertyType = await _context.PropertyTypeRealEstate
				.Include(x => x.ProductsRealEstate)
				.FirstOrDefaultAsync(x => x.Id == id);

			var serviceResponse = new ServiceResponse<GetPropertyTypeRealEstateDTO>()
			{
				Data = _mapper.Map<GetPropertyTypeRealEstateDTO>(propertyType)
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetPropertyTypeRealEstateDTO>> UpdatePropertyTypeRealEstate(UpdatePropertyTypeRealEstateDTO updatedPropertyType)
		{
			var serviceResponse = new ServiceResponse<GetPropertyTypeRealEstateDTO>();

			try
			{
				var propertyType = await _context.PropertyTypeRealEstate
					.FirstOrDefaultAsync(p => p.Id == updatedPropertyType.Id);
				if (propertyType is null) { throw new Exception($"Subcategory with Id '{updatedPropertyType.Id}' not found"); }

				propertyType.Name = updatedPropertyType.Name;
				propertyType.Description = updatedPropertyType.Description;

				await _context.SaveChangesAsync();

				serviceResponse.Data = _mapper.Map<GetPropertyTypeRealEstateDTO>(propertyType);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetPropertyTypeRealEstateDTO>>> DeletePropertyTypeRealEstate(int id)
		{
			var serviceResponse = new ServiceResponse<List<GetPropertyTypeRealEstateDTO>>();

			try
			{
				var propertyType = await _context.PropertyTypeRealEstate.FirstOrDefaultAsync(x => x.Id == id);
				if (propertyType is null) { throw new Exception($"Subcategory with Id '{id}' not found"); }

				_context.PropertyTypeRealEstate.Remove(propertyType);

				await _context.SaveChangesAsync();

				serviceResponse.Data = await _context.PropertyTypeRealEstate
					.Select(sc => _mapper.Map<GetPropertyTypeRealEstateDTO>(sc)).ToListAsync();
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
		}
	}
}
