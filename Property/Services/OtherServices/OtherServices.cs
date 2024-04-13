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

		public async Task<ServiceResponse<TransactionType>> GetTransactionTyoeByProductRealEstateId(int productId)
		{
			var serviceResponse = new ServiceResponse<TransactionType>();

			try
			{
				var product = await _context.ProductsRealEstate.
					Include(x => x.TransactionType)
					.FirstOrDefaultAsync(x => x.Id == productId);

				if (product != null)
				{
					var transactionType = await _context.TransactionTypes.FirstOrDefaultAsync(t => t.Id == product.TransactionType.Id);
					if(transactionType != null)
					{
						serviceResponse.Data = transactionType;
						return serviceResponse;
					}
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
