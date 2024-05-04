using Application.ErrorHandler;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCase
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoryService(ICategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }

        public async Task<bool> CategoryExist(int categoryId)
        {
            return await _categoryQuery.CategoryExist(categoryId);
        }
        public async Task EnsureCategoryExists(int categoryId)
        {
            bool exists = await _categoryQuery.CategoryExist(categoryId);
            if (!exists)
            {
                throw new CategoryDoesNotExistException(categoryId);
            }
        }
        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _categoryQuery.GetCategoryById(categoryId);
        }

        public async Task<ProductCategory> GetProductCategory(int categoryId)
        {
           var category = await _categoryQuery.GetCategoryById(categoryId);
           var productCategory = new ProductCategory
           {
                id = category.CategoryId,
                name = category.Name
           };
           return productCategory;  
        }
    }
}
