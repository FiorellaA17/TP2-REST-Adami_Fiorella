using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly StoreDbContext _context;

        public ProductQuery(StoreDbContext Context)
        {
            _context = Context;
        }
        public IQueryable<Product> GetListProducts()
        {
            return _context.Products.Include(p => p.CategoryName);
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = _context.Products
                .Include(p => p.CategoryName)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            return await product;
        }

        public async Task<bool> ProductExistsByName(string name)
        {
            return await _context.Products.AnyAsync(p => p.Name == name);
        }

        public async Task<string> GetCategoryNameById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return "Categoria no encontrada."; 
            }

            return category.Name;
        }

    }
}
