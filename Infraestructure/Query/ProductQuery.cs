using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly StoreDbContext _context;

        public ProductQuery(StoreDbContext Context)
        {
            _context = Context;
        }
        public async Task<List<ProductDto>> GetListProducts()
        {
            var products = await _context.Products
                .Include(p => p.CategoryName)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Category = p.Category,
                    Discount = p.Discount,
                    CategoryName = p.CategoryName.Name
                                                      
                })
                .ToListAsync();

            return products;
        }
    }
}
