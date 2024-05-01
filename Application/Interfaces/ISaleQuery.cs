using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISaleQuery
    {
        Task<IEnumerable<Sale>> GetSales(DateTime? from, DateTime? to);
    }
}
