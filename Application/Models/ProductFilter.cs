
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ProductFilter
    {
        [SwaggerParameter("Filtrar productos por categorías. Es posible filtrar por 1 o más categorias. Filtro opcional")]
        public int[]? categories { get; set; } = new int[0];

        [SwaggerParameter("Filtrar productos por nombre. Es posible filtrar por nombres incompletos")]
        public string? name { get; set; } = "";

        [SwaggerParameter("Limita el número de productos devueltos. Default = 0")]
        [Range(0, int.MaxValue)]
        public int limit { get; set; } = 0;

        [SwaggerParameter("Número de productos a omitir antes de empezar a devolver los resultados. Default = 0")]
        [Range(0, int.MaxValue)]
        public int offset { get; set; } = 0;
    }
}
