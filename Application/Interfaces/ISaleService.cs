using Application.Models;

namespace Application.Interfaces
{
    public interface ISaleService
    {
        Task<SaleResponse> GenerateSale(SaleRequest request);
        Task<IEnumerable<SaleGetResponse>> GetSales(DateTime? from, DateTime? to);
    }
}
