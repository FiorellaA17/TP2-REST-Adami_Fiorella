
namespace Application.Models
{
    public class SaleGetResponse
    {
        public int id { get; set; }
        public decimal totalPay { get; set; }
        public decimal totalQuantity { get; set; }
        public DateTime date { get; set; }
    }
}
