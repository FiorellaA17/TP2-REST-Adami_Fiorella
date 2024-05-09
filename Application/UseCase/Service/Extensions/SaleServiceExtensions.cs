using Application.ErrorHandler;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCase.Service.Extensions
{
    public class SaleServiceExtensions : ISaleServiceExtensions
    {
        private readonly ISaleQuery _saleQuery;

        public SaleServiceExtensions(ISaleQuery saleQuery)
        {
            _saleQuery = saleQuery;
        }

        public void ValidatePaymentAmountsMatch(SaleRequest request, Sale sale, List<string> errors)
        {
            if (sale.TotalPay != request.totalPayed)
            {
                errors.Add($"El total calculado no coincide con el total pagado ({request.totalPayed}).");
            }
        }

        public void ValidateProductQuantity(SaleProductRequest request, List<string> errors)
        {
            if (request.quantity <= 0)
            {
                errors.Add($"La cantidad para el producto con ID {request.productId} debe ser mayor a cero.");
            }
        }

        public void ProductNotFound(Guid id, List<string> errors)
        {
            errors.Add($"Producto con ID {id} no encontrado.");
        }

        public void ListErrors(List<string> errors)
        {
            if (errors.Any())
            {
                throw new BadRequestException(string.Join(" ", errors));
            }
        }

        public async Task<Sale> ValidateSaleExist(int id)
        {
            var sale = await _saleQuery.GetSaleById(id);
            if (sale == null)
            {
                throw new SaleNotFoundException(id);
            }
            return sale;
        }

          
}
}
