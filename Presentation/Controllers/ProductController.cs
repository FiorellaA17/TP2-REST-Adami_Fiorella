using Application.ErrorHandler;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetProducts([FromQuery] ProductFilter filter)
        //{
        //    var products = await _productService.GetFilteredProducts(filter);
        //    return Ok(products);
        //}
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene una lista de productos.",
            Description = "Recuperar una lista de productos disponibles, con opciones de filtrado."
        )]
        [SwaggerResponse(200, Description = "Éxito al recuperar los productos.", Type = typeof(IEnumerable<ProductGetResponse>))]
        [SwaggerResponse(400, Description = "Parámetros de solicitud inválidos")]
        [SwaggerResponse(500, Description = "Error interno del servidor")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve un 400 con los detalles del error de validación
            }

            try
            {
                var products = await _productService.GetFilteredProducts(filter);
                return Ok(products); //200
            }
            catch (Exception) // Captura excepciones generales
            {
                return StatusCode(500, "Internal server error "); // Devuelve un 500 en caso de error no manejado
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
        {

            if (!ModelState.IsValid)
            {
                var apiError = new ApiError("Solicitud incorrecta.");
                return BadRequest(apiError);
            }

            try
            {
                var productResponse = await _productService.CreateProduct(request);
                return CreatedAtRoute(new { id = productResponse.id }, productResponse); // 201 Created
            }
            catch (ProductAlreadyExistsException)
            {
                var apiError = new ApiError("Conflicto, el producto ya existe.");
                return Conflict(apiError);
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiError("Error interno del servidor."));
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetails(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return BadRequest(new ApiError("Formato de Id inválido."));
            }

            var productDetails = await _productService.GetProductById(guidId);
            if (productDetails == null)
            {
                return NotFound(new ApiError("Producto no encontrado."));
            }

            return Ok(productDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, ProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError("Solicitud incorrecta."));
            }
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return BadRequest(new ApiError("Formato de Id inválido."));
            }

            var productDetails = await _productService.GetProductById(guidId);
            if (productDetails == null)
            {
                return NotFound(new ApiError("Producto no encontrado."));
            }

            try
            {
                var updatedProduct = await _productService.UpdateProduct(guidId, request);
                return Ok(updatedProduct);
            }
            catch (ProductAlreadyExistsException ex)
            {
                return Conflict(new ApiError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var response = await _productService.DeleteProduct(id);
                return Ok(response);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new ApiError(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500,new ApiError("Ocurrió un error al intentar eliminar el producto."));
            }
        }
    }
}
