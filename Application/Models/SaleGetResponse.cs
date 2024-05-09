

namespace Application.Models
{
    public class SaleGetResponse
    {
        public int id { get; set; }
        public decimal totalPay { get; set; }
        public int totalQuantity { get; set; }
        public DateTime date { get; set; }
    }
}
