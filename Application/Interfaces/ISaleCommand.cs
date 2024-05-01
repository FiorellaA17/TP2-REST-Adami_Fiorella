using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISaleCommand
    {
        Task AddSale(Sale sale);
    }
}
