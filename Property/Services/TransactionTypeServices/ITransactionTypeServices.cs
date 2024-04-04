using Property.DTOs.Store;
using Property.DTOs.TransactionType;
using Property.Models;

namespace Property.Services.TransactionTypeServices
{
    public interface ITransactionTypeServices
    {
        Task<ServiceResponse<List<GetTransactionTypeDTO>>> GetAllTransactionTypes();
        Task<ServiceResponse<GetTransactionTypeDTO>> GetTransactionTypeById(int id);
        Task<ServiceResponse<List<GetTransactionTypeDTO>>> AddTransactionType(AddTransactionTypeDTO newTransactionType);
        Task<ServiceResponse<GetTransactionTypeDTO>> UpdateTransactionType(UpdateTransactionTypeDTO updatedTransactionType);
        Task<ServiceResponse<List<GetTransactionTypeDTO>>> DeleteTransactionType(int id);
    }
}
