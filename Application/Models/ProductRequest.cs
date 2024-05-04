

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ProductRequest
    {
        public string name { get; set; }
        public string? description { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        public decimal price { get; set; }

        [DefaultValue(0)]
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public int discount { get; set; }
        public string imageUrl { get; set; }
        public int category { get; set; } 
    }
}
