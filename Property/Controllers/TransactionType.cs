using Microsoft.AspNetCore.Mvc;
using Property.DTOs.TransactionType;
using Property.Services.TransactionTypeServices;

namespace Property.Controllers
{
    public class TransactionTypeController : Controller
    {
        private readonly ITransactionTypeServices _transactionTypeServices;

        public TransactionTypeController(ITransactionTypeServices transactionTypeServices)
        {
            _transactionTypeServices = transactionTypeServices;
        }

        public IActionResult AddTransactionType()
        {
            return View();
        }

        public async Task<IActionResult> GetTransactionType()
        {
            var transationTypes = await _transactionTypeServices.GetAllTransactionTypes();
            return View(transationTypes.Data);
        }

        public async Task<IActionResult> UpdateTransactionType(int id)
        {
            var transationType = await _transactionTypeServices.GetTransactionTypeById(id);
            return View(transationType.Data);
        }

        [HttpPost]
        public IActionResult SaveAddTransactionType(AddTransactionTypeDTO newTransationType)
        {
            _transactionTypeServices.AddTransactionType(newTransationType);

            return RedirectToAction("GetTransactionType");
        }

        [HttpPost]
        public async Task<IActionResult> SaveUpdateTransactionType(UpdateTransactionTypeDTO updatedTransationType)
        {
            await _transactionTypeServices.UpdateTransactionType(updatedTransationType);
            return RedirectToAction("GetTransactionType");
        }

        public async Task<IActionResult> DeleteTransactionType(int id)
        {
            await _transactionTypeServices.DeleteTransactionType(id);
            return RedirectToAction("GetTransactionType");
        }
    }
}
