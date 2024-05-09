using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISaleService
    {
        Task<SaleResponse> GenerateSale(SaleRequest request);
        Task CalculateSale(SaleRequest request, Sale sale, List<string> errors);
        Task<IEnumerable<SaleGetResponse>> GetSales(DateTime? from, DateTime? to);
        Task<SaleResponse> GetSaleById(int id);
    }
}
