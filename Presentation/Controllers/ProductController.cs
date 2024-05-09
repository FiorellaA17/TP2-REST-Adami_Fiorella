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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene una lista de productos.",
            Description = "Recuperar una lista de productos disponibles, con opciones de filtrado."
        )]
        [SwaggerResponse(200, Description = "Éxito al recuperar los productos.", Type = typeof(IEnumerable<ProductGetResponse>))]
        public async Task<IActionResult> GetProducts([FromQuery] ProductFilter filter)
        {
                var products = await _productService.GetFilteredProducts(filter);
                return Ok(products);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un nuevo producto.",
            Description = "Permite la creación de un nuevo producto en el sistema."
        )]
        [SwaggerResponse(201, Description = "Producto creado con éxito", Type = typeof(ProductResponse))]
        [SwaggerResponse(400, Description = "Solicitud incorrecta.", Type = typeof(ApiError))]
        [SwaggerResponse(409, Description = "Conflicto, el producto ya existe.", Type = typeof(ApiError))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
        {
            try
            {
                var productResponse = await _productService.CreateProduct(request);
                return CreatedAtRoute(new { id = productResponse.id }, productResponse);
            }

            catch (ProductAlreadyExistsException ex)
            {
                return Conflict(new ApiError(ex.Message));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene detalles de un producto específico.",
            Description = "Recupera los detalles de un producto por su ID único."
        )]
        [SwaggerResponse(200, Description = "Éxito al recuperar los detalles del producto.", Type = typeof(ProductResponse))]
        [SwaggerResponse(404, Description = "Producto no encontrado.", Type = typeof(ApiError))]
        public async Task<IActionResult> GetProductDetails(Guid id)
        {
            try
            {
                var productDetails = await _productService.GetProductById(id);
                return Ok(productDetails);
            }

            catch(ProductNotFoundException ex)
            {
                return NotFound(new ApiError(ex.Message));
            } 
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un producto existente.",
            Description = "Permite la actualización de los detalles de un producto específico."
        )]
        [SwaggerResponse(200, Description = "Producto actualizado con éxito.", Type = typeof(ProductResponse))]
        [SwaggerResponse(400, Description = "Solicitud incorrecta.", Type = typeof(ApiError))]
        [SwaggerResponse(404, Description = "Producto no encontrado.", Type = typeof(ApiError))]
        [SwaggerResponse(409, Description = "Conflicto al actualizar el producto.", Type = typeof(ApiError))]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductRequest request)
        {

            try
            {
                var updatedProduct = await _productService.UpdateProduct(id, request);
                return Ok(updatedProduct);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new ApiError(ex.Message));
            }
            catch (ProductAlreadyExistsException ex)
            {
                return Conflict(new ApiError(ex.Message));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un producto específico.",
            Description = "Permite la eliminación de un producto del sistema usando su ID."
        )]
        [SwaggerResponse(200, Description = "Producto eliminado con éxito.", Type = typeof(ProductResponse))]
        [SwaggerResponse(404, Description = "Producto no encontrado.", Type = typeof(ApiError))]
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
            catch (ProductHasSalesHistoryException ex)
            {
                return Conflict(new ApiError(ex.Message));
            }
        }
    }
}
