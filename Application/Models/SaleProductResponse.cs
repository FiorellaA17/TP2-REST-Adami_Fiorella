

namespace Application.Models
{
    public class SaleProductResponse
    {
        public int id { get; set; }
        public Guid productId { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
    }
}
