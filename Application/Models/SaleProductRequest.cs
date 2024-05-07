

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class SaleProductRequest
    {
        public Guid productId { get; set; }

        [DefaultValue(0)]
        [Range(0, int.MaxValue, ErrorMessage = "El valor de la cantidad debe ser mayor a 0.")]
        public int quantity { get; set; }
    }
}
