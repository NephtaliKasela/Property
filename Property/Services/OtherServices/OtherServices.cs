using AutoMapper;
using Property.Data;
using Property.DTOs.Category;
using Property.Models;
using Microsoft.EntityFrameworkCore;

namespace Property.Services.OtherServices
{
    public class OtherServices : IOtherServices
    {
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public OtherServices(DataContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
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

		public async Task<ServiceResponse<Category>> GetCategoryById(string categoryId)
		{
			var serviceResponse = new ServiceResponse<Category>();

			try
			{
				bool result; int number;
				(result, number) = CheckIfInteger(categoryId);
				if(result)
				{
					var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == number);

					if (category is null) { throw new Exception($"Category with Id '{number}' not found"); }

					serviceResponse.Data =category;
					return serviceResponse;
				}
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
