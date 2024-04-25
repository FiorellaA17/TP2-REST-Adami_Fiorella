
namespace Application.Models
{
    public class ProductFilter
    {
        public string? NameFilter { get; set; } = "";
        public List<int?> CategoryIds { get; set; } = new List<int?>();
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}
