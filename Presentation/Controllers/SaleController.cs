using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SaleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            try
            {
                var saleResponse = await _saleService.GenerateSale(request);
                return Ok(saleResponse);
            }
            catch (Exception)
            {
                //_logger.LogError(ex, "Failed to create sale.");
                return StatusCode(500, "An internal error occurred.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSales([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            try
            {
                var sales = await _saleService.GetSales(from, to);
                return Ok(sales);  // Devuelve el código de estado 200 junto con los datos
            }
            catch (Exception ex)
            {
                // Asegúrate de logear el error antes de responder
                // _logger.LogError(ex, "Failed to get sales.");  // Considera inyectar ILogger<SalesController> si aún no lo has hecho
                return StatusCode(500, "Internal Server Error");  // Devuelve el código de estado 500
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(int id)
        {
            var sale = await _saleService.GetSaleById(id);
            if (sale == null)
            {
                return NotFound(new ApiError("Sale not found"));
            }
            return Ok(sale);
        }
    }
}
