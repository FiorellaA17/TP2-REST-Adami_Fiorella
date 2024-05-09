using Application.Models;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IProductServiceExtensions
    {
        IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductFilter filter);
        Task ValidateProductRequest(ProductRequest request);
        Task ValidateProductDoesNotExistByName(ProductRequest request);
        Task ValidateProductHasSaleHistory(Guid id);
        Task ValidateProductExist(Guid id);
        Task ValidateNameUpdate(Guid id, ProductRequest request);
    }
}
