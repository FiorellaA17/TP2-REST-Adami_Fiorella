
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class SaleRequest
    {
        public List<SaleProductRequest> products { get; set; } = new List<SaleProductRequest>();

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "El valor del totalPayed debe ser mayor a 0.")]
        public decimal totalPayed { get; set; }
    }
}
