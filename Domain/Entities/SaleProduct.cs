using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SaleProduct
    {
        public int ShoppingCartId { get; set; }
        public int Sale { get; set; }
        public Guid Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public Sale SaleName { get; set; }
        public Product ProductName { get; set; }
    }
}
