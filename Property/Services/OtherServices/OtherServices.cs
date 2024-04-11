using AutoMapper;
using Property.Data;
using Property.DTOs.Category;
using Property.Models;
using Microsoft.EntityFrameworkCore;
using Property.Services.TransactionTypeServices;

namespace Property.Services.OtherServices
{
    public class OtherServices : IOtherServices
    {
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		private readonly ITransactionTypeServices _transactionTypeServices;

		public OtherServices(DataContext context, IMapper mapper, ITransactionTypeServices transactionTypeServices)
        {
			_context = context;
			_mapper = mapper;
			_transactionTypeServices = transactionTypeServices;
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
