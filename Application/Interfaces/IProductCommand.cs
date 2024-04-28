using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductCommand
    {
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
