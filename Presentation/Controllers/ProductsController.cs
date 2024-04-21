using Application.Interfaces;
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
        public async Task<IActionResult> GetListProducts()
        {
            var products = await _productService.GetListProducts();
            return Ok(products);
        }
    }

}
