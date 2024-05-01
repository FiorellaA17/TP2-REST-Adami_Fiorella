
namespace Application.Models
{
    public class ProductGetResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int discount { get; set; }
        public string? imageUrl { get; set; }
        public int categoryName { get; set; }
    }
}
