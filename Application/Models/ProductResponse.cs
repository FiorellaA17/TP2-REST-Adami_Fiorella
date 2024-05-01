

namespace Application.Models
{
    public class ProductResponse
    {
        public string id { get; set; }

        public string? name { get; set; }

        public string? description { get; set; }

        public decimal price { get; set; }

        public int discount { get; set; }

        public string? imageUrl { get; set; }

        public ProductCategory category { get; set; }
    }
}