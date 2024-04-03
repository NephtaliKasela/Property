using AutoMapper;
using Property.Data;
using Property.DTOs.Category;
using Property.DTOs.Subcategories.SubcategoryRealEstate;
using Property.Models;
using Property.Models.Subcategories;
using Property.Services.CategoryServices;
using Property.Services.OtherServices;
using Property.Services.SubCategoryServicesRealEstate;
using Microsoft.EntityFrameworkCore;

namespace Property.Services.SubCategoryServices
{
	public class SubCategoryServicesRealEstate : ISubCategoryServicesRealEstate
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		private readonly ICategoryServices _categoryServices;
		private readonly IOtherServices _otherServices;

		public SubCategoryServicesRealEstate(DataContext context, IMapper mapper, ICategoryServices categoryServices, IOtherServices otherServices)
		{
			_context = context;
			_mapper = mapper;
			_categoryServices = categoryServices;
			_otherServices = otherServices;
		}

		//public async Task<ServiceResponse<List<GetSubcategoryRealEstateDTO>>> AddSubcategoryRealEstate(AddSubcategoryRealEstateDTO newSubcategory, GetCategoryDTO category)
		public async Task<ServiceResponse<List<GetSubcategoryRealEstateDTO>>> AddSubcategoryRealEstate(AddSubcategoryRealEstateDTO newSubcategory)
		{
			var serviceResponse = new ServiceResponse<List<GetSubcategoryRealEstateDTO>>();
			var subcategory = _mapper.Map<SubcategoryRealEstate>(newSubcategory);

			bool result; int number;

			// Get Category
			(result, number) = _otherServices.CheckIfInteger(newSubcategory.CategoryId);
			if (result == true)
			{
				var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == number);
				if (category != null)
				{
					subcategory.Category = category;
				}
			}

			await _context.SubcategoriesRealEstate.AddAsync(subcategory);
			await _context.SaveChangesAsync();

			serviceResponse.Data = await _context.SubcategoriesRealEstate
				.Select(p => _mapper.Map<GetSubcategoryRealEstateDTO>(p)).ToListAsync();

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetSubcategoryRealEstateDTO>>> GetAllSubcategoriesRealEstate()
		{
			var subcategories = await _context.SubcategoriesRealEstate
				.Include(x => x.Category)
				.Include(x => x.ProductsRealEstate)
				.ToListAsync();
			var serviceResponse = new ServiceResponse<List<GetSubcategoryRealEstateDTO>>()
			{
				Data = subcategories.Select(p => _mapper.Map<GetSubcategoryRealEstateDTO>(p)).ToList()
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetSubcategoryRealEstateDTO>> GetSubcategoryRealEstateById(int id)
		{
			var subcategory = await _context.SubcategoriesRealEstate
				.Include(x => x.Category)
				.Include(x => x.ProductsRealEstate)
				.FirstOrDefaultAsync(x => x.Id == id);

			var serviceResponse = new ServiceResponse<GetSubcategoryRealEstateDTO>()
			{
				Data = _mapper.Map<GetSubcategoryRealEstateDTO>(subcategory)
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetSubcategoryRealEstateDTO>> UpdateSubcategoryRealEstate(UpdateSubcategoryRealEstateDTO updatedSubcategory)
		{
			var serviceResponse = new ServiceResponse<GetSubcategoryRealEstateDTO>();

			try
			{
				var subcategory = await _context.SubcategoriesRealEstate
					.FirstOrDefaultAsync(p => p.Id == updatedSubcategory.Id);
				if (subcategory is null) { throw new Exception($"Subcategory with Id '{updatedSubcategory.Id}' not found"); }

				subcategory.Name = updatedSubcategory.Name;
				subcategory.Description = updatedSubcategory.Description;

				bool result; int number;

				// Get Category
				(result, number) = _otherServices.CheckIfInteger(updatedSubcategory.CategoryId);
				if (result == true)
				{
					var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == number);
					if (category is not null)
					{
						subcategory.Category = category;
					}
				}

				await _context.SaveChangesAsync();

				serviceResponse.Data = _mapper.Map<GetSubcategoryRealEstateDTO>(subcategory);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetSubcategoryRealEstateDTO>>> DeleteSubcategoryRealEstate(int id)
		{
			var serviceResponse = new ServiceResponse<List<GetSubcategoryRealEstateDTO>>();

			try
			{
				var subcategory = await _context.SubcategoriesRealEstate.FirstOrDefaultAsync(sc => sc.Id == id);
				if (subcategory is null) { throw new Exception($"Subcategory with Id '{id}' not found"); }

				_context.SubcategoriesRealEstate.Remove(subcategory);

				await _context.SaveChangesAsync();

				serviceResponse.Data = await _context.SubcategoriesRealEstate
					.Select(sc => _mapper.Map<GetSubcategoryRealEstateDTO>(sc)).ToListAsync();
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
