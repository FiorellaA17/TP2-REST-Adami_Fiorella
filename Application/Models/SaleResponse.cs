

namespace Application.Models
{
    public class SaleResponse
    {
        public int id { get; set; }
        public decimal totalPay { get; set; }
        public int totalQuantity { get; set; }
        public decimal subtotal { get; set; }
        public decimal totalDiscount { get; set; }
        public decimal taxes { get; set; }
        public DateTime date { get; set; }
        public List<SaleProductResponse> products { get; set; } = new List<SaleProductResponse>();
    }
}
