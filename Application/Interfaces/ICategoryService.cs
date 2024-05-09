using Application.Models;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ProductCategory> GetProductCategory(int categoryId);
        Task<bool> CategoryExists(int categoryId);
        Task<Category> GetCategoryById(int categoryId);
    }
}
