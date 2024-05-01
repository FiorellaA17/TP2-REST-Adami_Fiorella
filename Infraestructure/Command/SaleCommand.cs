using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Command
{
    public class SaleCommand : ISaleCommand
    {
        private readonly StoreDbContext _context;

        public SaleCommand(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddSale(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }
    }
}
