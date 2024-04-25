

namespace Application.Models
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string ImageUrl { get; set; }

        public ProductCategoryResponse Category { get; set; }
    }
    public class ProductCategoryResponse
    {
        public string Name { get; set; }
    }
}