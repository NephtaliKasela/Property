using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property.Data;
using Property.DTOs.TransactionType;
using Property.Models;

namespace Property.Services.TransactionTypeServices
{
    public class TransactionTypeServices : ITransactionTypeServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransactionTypeServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetTransactionTypeDTO>>> GetAllTransactionTypes()
        {
            var transactionTypes = await _context.TransactionTypes
                .Include(x => x.ProductsRealEstate)
                .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetTransactionTypeDTO>>()
            {
                Data = transactionTypes.Select(s => _mapper.Map<GetTransactionTypeDTO>(s)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTransactionTypeDTO>> GetTransactionTypeById(int id)
        {
            var transactionType = await _context.TransactionTypes
                .Include(x => x.ProductsRealEstate)
                .FirstOrDefaultAsync(x => x.Id == id);

            var serviceResponse = new ServiceResponse<GetTransactionTypeDTO>()
            {
                Data = _mapper.Map<GetTransactionTypeDTO>(transactionType)
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTransactionTypeDTO>>> AddTransactionType(AddTransactionTypeDTO newTransactionType)
        {
            var serviceResponse = new ServiceResponse<List<GetTransactionTypeDTO>>();
            var transactionType = _mapper.Map<TransactionType>(newTransactionType); 

            transactionType.Name = transactionType.Name;

            _context.TransactionTypes.Add(transactionType);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.TransactionTypes
                .Select(s => _mapper.Map<GetTransactionTypeDTO>(s)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTransactionTypeDTO>> UpdateTransactionType(UpdateTransactionTypeDTO updatedTransactionType)
        {
            var serviceResponse = new ServiceResponse<GetTransactionTypeDTO>();

            try
            {
                var transactionType = await _context.TransactionTypes
                    .FirstOrDefaultAsync(c => c.Id == updatedTransactionType.Id);
                if (transactionType is null) { throw new Exception($"TransactionType with Id '{updatedTransactionType.Id}' not found"); }

                transactionType.Name = updatedTransactionType.Name;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetTransactionTypeDTO>(transactionType);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTransactionTypeDTO>>> DeleteTransactionType(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTransactionTypeDTO>>();

            try
            {
                var transactionType = await _context.TransactionTypes.FirstOrDefaultAsync(s => s.Id == id);
                if (transactionType is null) { throw new Exception($"TransactionType with Id '{id}' not found"); }

                _context.TransactionTypes.Remove(transactionType);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.TransactionTypes
                    .Select(c => _mapper.Map<GetTransactionTypeDTO>(c)).ToListAsync();
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
