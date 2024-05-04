
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ProductFilter
    {
        [SwaggerParameter("Filtrar productos por categorías. Es posible filtrar por 1 o más categorias. Filtro opcional")]
        public int[]? categories { get; set; }

        [SwaggerParameter("Filtrar productos por nombre. Es posible filtrar por nombres incompletos")]
        public string? name { get; set; }

        [SwaggerParameter("Limita el número de productos devueltos.")]
        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        public int limit { get; set; }

        [SwaggerParameter("Número de productos a omitir antes de empezar a devolver los resultados.")]
        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        public int offset { get; set; }
    }
}
