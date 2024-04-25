using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Command
{
    public class ProductCommand : IProductCommand
    {
        private readonly StoreDbContext _context;

        public ProductCommand(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}
