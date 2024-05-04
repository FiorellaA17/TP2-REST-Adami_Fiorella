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
        [SwaggerResponse(400, Description = "Solicitud incorrecta.")]
        [SwaggerResponse(500, Description = "Error interno del servidor")]
        public async Task<IActionResult> GetSales([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError("Solicitud incorrecta.")); //400
            }

            try
            {
                var sales = await _saleService.GetSales(from, to);
                return Ok(sales);  // 200
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");  // 500
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Registra una nueva venta.",
            Description = "Permite ingresar una nueva venta al sistema."
        )]
        [SwaggerResponse(201, Description = "Venta registrada con éxito.", Type = typeof(SaleResponse))]
        [SwaggerResponse(400, Description = "Solicitud incorrecta.")]
        [SwaggerResponse(500, Description = "Error interno del servidor")]
        public async Task<IActionResult> CreateSale([FromBody] SaleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError("Solicitud incorrecta.")); //400
            }

            try
            {
                var saleResponse = await _saleService.GenerateSale(request);
                return CreatedAtRoute(new { id = saleResponse.id }, saleResponse); //201
            }

            catch(PaymentMismatchException ex)
            {
                return Conflict(new ApiError(ex.Message));//409
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new ApiError(ex.Message)); //404
            }
            catch (Exception)
            {
                return StatusCode(500, "An internal error occurred.");
            }
        }  

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene detalles de una venta específica.",
            Description = "Recupera los detalles de una venta por su ID único."
        )]
        [SwaggerResponse(200, Description = "Éxito al recuperar los detalles de la venta.", Type = typeof(SaleResponse))]
        [SwaggerResponse(404, Description = "Venta no encontrada.")]
        [SwaggerResponse(500, Description = "Error interno del servidor")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            try
            {
                var sale = await _saleService.GetSaleById(id);
                return Ok(sale); //200
            }
            
            catch (SaleNotFoundException ex)
            {
                return NotFound(new ApiError(ex.Message)); //404
            }
        }
    }
}
