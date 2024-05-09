using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using System.Data;

namespace Application.UseCase
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoryService(ICategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }
        public async Task<bool> CategoryExists(int categoryId)
        {
            var category = await _categoryQuery.GetCategoryById(categoryId);
            return category != null;
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
