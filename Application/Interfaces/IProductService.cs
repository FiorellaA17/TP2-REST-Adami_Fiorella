using Application.Models;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductGetResponse>> GetFilteredProducts(ProductFilter filter);
        Task<ProductResponse> CreateProduct(CreateProductDto productDto);
        Task<ProductDto> GetProductById(Guid productId);
    }
}
