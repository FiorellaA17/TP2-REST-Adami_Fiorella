
namespace Application.Models
{
    public class ProductGetResponse
    {
        public String Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
