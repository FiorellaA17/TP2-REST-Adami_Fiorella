using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISaleQuery
    {
        Task<IEnumerable<Sale>> GetSalesFromTo(DateTime? from, DateTime? to);
        Task<Sale> GetSaleById(int id);
    }
}
