using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class SaleQuery : ISaleQuery
    {
        private readonly StoreDbContext _context;

        public SaleQuery(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetSalesFromTo(DateTime? from, DateTime? to)
        {
            if (to.HasValue)
            {
                to = to.Value.AddDays(1).Date; // Mueve 'to' al comienzo del siguiente día
            }

            return await _context.Sales
                        .Include(s => s.SaleProducts)
                        .Where(s => (!from.HasValue || s.Date >= from) && (!to.HasValue || s.Date < to))
                        .ToListAsync();
        }

        public async Task<Sale> GetSaleById(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleProducts)
                .FirstOrDefaultAsync(s => s.SaleId == id);
            return sale;
        }
    }
}
