
namespace Application.Models
{
    public class ProductFilter
    {
        public int[]? Categories { get; set; } = new int[0];
        public string? Name { get; set; } = "";
        //public List<int?> CategoryIds { get; set; } = new List<int?>();
        //public int Limit { get; set; } = 10;
        public int Limit { get; set; } = 0;
        public int Offset { get; set; } = 0;
    }
}
