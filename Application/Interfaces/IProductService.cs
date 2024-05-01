using Application.Models;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductGetResponse>> GetFilteredProducts(ProductFilter filter);
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<ProductResponse> GetProductById(Guid productId);
        Task<ProductResponse> UpdateProduct(Guid id, ProductRequest request);
        Task<ProductResponse> DeleteProduct(Guid productId);
    }
}
