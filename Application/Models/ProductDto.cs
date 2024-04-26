

namespace Application.Models
{
    public class ProductDto
    {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public int Category { get; set; }
            public int Discount { get; set; }
            public string ImageUrl { get; set; }
    }
}
