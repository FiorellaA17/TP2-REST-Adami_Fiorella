using Application.ErrorHandler;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductFilter filter)
        {
            var products = await _productService.GetFilteredProducts(filter);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
        {
            try
            {
                var productResponse = await _productService.CreateProduct(request);
                return CreatedAtRoute(new { id = productResponse.Id }, productResponse);
            }
            catch (ProductAlreadyExistsException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetails(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid product ID.");
            }

            var productDetails = await _productService.GetProductById(id);
            if (productDetails == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(productDetails);
        }      
    }
}
