using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly StoreDbContext _context;

        public CategoryQuery(StoreDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> CategoryExist(int categoryId)
        //{           
        //    return await _context.Categories.AnyAsync(c => c.CategoryId == categoryId);
        //}

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }
    }
}
