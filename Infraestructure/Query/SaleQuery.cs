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

        public async Task<IEnumerable<Sale>> GetSales(DateTime? from, DateTime? to)
        {
            //var query = _context.Sales.AsQueryable();

            //if (from.HasValue)
            //    query = query.Where(s => s.Date >= from.Value);

            //if (to.HasValue)
            //    query = query.Where(s => s.Date <= to.Value);

            //return await query.Include(s => s.SaleProducts).ToListAsync();
            return await _context.Sales
                          .Include(s => s.SaleProducts)
                          .Where(s => (!from.HasValue || s.Date >= from) && (!to.HasValue || s.Date <= to))
                          .ToListAsync();
        }
    }
}
