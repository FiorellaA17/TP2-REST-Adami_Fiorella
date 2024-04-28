

namespace Application.Models
{
    public class ProductResponse
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public string? ImageUrl { get; set; }

        public ProductCategory Category { get; set; }
    }
    public class ProductCategory
    {
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}