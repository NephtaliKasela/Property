using Property.DTOs.Category;
using Property.Models;

namespace Property.Services.OtherServices
{
    public interface IOtherServices
    {
        (bool, int) CheckIfInteger(string number);
		Task<ServiceResponse<Category>> GetCategoryById(string categoryId);

	}
}
