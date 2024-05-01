using Application.Models;


namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ProductCategory> GetProductCategory(int categoryId); 
    }
}
