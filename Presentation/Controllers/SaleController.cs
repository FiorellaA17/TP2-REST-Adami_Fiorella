using Application.ErrorHandler;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado de ventas.",
            Description = "Recupera un resumen de las ventas realizadas, con opción de filtrado por fecha."
        )]
        [SwaggerResponse(200, Description = "Éxito al recuperar las ventas.", Type = typeof(IEnumerable<SaleGetResponse>))]
        [SwaggerResponse(400, Description = "Solicitud incorrecta.", Type = typeof(ApiError))]
        public async Task<IActionResult> GetSales([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            if (from.HasValue && to.HasValue && from > to)
            {
                return BadRequest(new ApiError("La fecha inicial no puede ser mayor que la fecha final."));
            }

            var sales = await _saleService.GetSales(from, to);
             return Ok(sales);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Registra una nueva venta.",
            Description = "Permite ingresar una nueva venta al sistema."
        )]
        [SwaggerResponse(201, Description = "Venta registrada con éxito.", Type = typeof(SaleResponse))]
        [SwaggerResponse(400, Description = "Solicitud incorrecta.", Type = typeof(ApiError))]
        public async Task<IActionResult> CreateSale([FromBody] SaleRequest request)
        {
            try
            {
                var saleResponse = await _saleService.GenerateSale(request);
                return CreatedAtRoute(new { id = saleResponse.id }, saleResponse);
            }

            catch(BadRequestException ex)
            {
                return BadRequest(new ApiError(ex.Message));
            }

        }  

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene detalles de una venta específica.",
            Description = "Recupera los detalles de una venta por su ID único."
        )]
        [SwaggerResponse(200, Description = "Éxito al recuperar los detalles de la venta.", Type = typeof(SaleResponse))]
        [SwaggerResponse(404, Description = "Venta no encontrada.", Type = typeof(ApiError))]
        public async Task<IActionResult> GetSaleById(int id)
        {
            try
            {
                var sale = await _saleService.GetSaleById(id);
                return Ok(sale);
            }
            
            catch (SaleNotFoundException ex)
            {
                return NotFound(new ApiError(ex.Message));
            }
        }
    }
}
