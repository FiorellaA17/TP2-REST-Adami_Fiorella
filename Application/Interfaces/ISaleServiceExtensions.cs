using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISaleServiceExtensions
    {
        public void ValidatePaymentAmountsMatch(SaleRequest request, Sale sale, List<string> errors);
        public void ListErrors(List<string> errors);
        public void ValidateProductQuantity(SaleProductRequest request, List<string> errors);
        public void ProductNotFound(Guid id, List<string> errors);
        Task<Sale> ValidateSaleExist(int id);


    }
}
