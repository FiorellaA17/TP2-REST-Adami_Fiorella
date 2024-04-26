using Application.Models;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductGetResponse>> GetFilteredProducts(ProductFilter filter);
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<ProductDto> GetProductById(Guid productId);
    }
}
