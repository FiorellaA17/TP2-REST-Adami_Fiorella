using Application.Interfaces;
using Application.Models;

namespace Application.UseCase
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoryService(ICategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
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
