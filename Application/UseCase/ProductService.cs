using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCase
{
    public class ProductService : IProductService
    {
        private readonly IProductQuery _QueryProduct;

        public ProductService(IProductQuery QueryProduct)
        {
            _QueryProduct = QueryProduct;
        }
        public Task<List<ProductDto>> GetListProducts()
        {
            return _QueryProduct.GetListProducts();
        }
    }
}
