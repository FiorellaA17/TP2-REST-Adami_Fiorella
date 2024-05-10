

namespace Application.Models
{
    public class SaleRequest
    {
        public List<SaleProductRequest>? products { get; set; } = new List<SaleProductRequest>();
        public decimal totalPayed { get; set; }
    }
}
