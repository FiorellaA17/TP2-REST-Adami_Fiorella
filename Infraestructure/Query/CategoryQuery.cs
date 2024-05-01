using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Query
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly StoreDbContext _context;

        public CategoryQuery(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }
    }
}
