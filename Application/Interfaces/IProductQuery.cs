using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductQuery
    {
        IQueryable<Product> GetListProducts();
        Task<Product> GetProductById(Guid id);
        Task<bool> ProductExistsByName(string name);
        Task<string> GetCategoryNameById(int categoryId);
    }
}
