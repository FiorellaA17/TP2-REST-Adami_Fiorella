using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.UseCase.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductQuery _productQuery;
        private readonly IProductCommand _productCommand;
        private readonly ICategoryService _categoryService;
        private readonly IProductServiceExtensions _productServiceExtensions;

        public ProductService(IProductQuery productQuery, IProductCommand productCommand, ICategoryService categoryService, IProductServiceExtensions productServiceExtensions)
        {
            _productQuery = productQuery;
            _productCommand = productCommand;
            _categoryService = categoryService;
            _productServiceExtensions = productServiceExtensions;
        }

        public async Task<IEnumerable<ProductGetResponse>> GetFilteredProducts(ProductFilter filter)
        {
            var query = _productQuery.GetListProducts();

            query = _productServiceExtensions.ApplyFilters(query, filter);

            var products = await query
           .Select(p => new ProductGetResponse
           {
               id = p.ProductId,
               name = p.Name,
               price = p.Price,
               discount = p.Discount,
               imageUrl = p.ImageUrl,
               categoryName = p.CategoryName.Name,
           }).ToListAsync();

            return products;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            await _productServiceExtensions.ValidateProductDoesNotExistByName(request);
            await _productServiceExtensions.ValidateProductRequest(request);

            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = request.name,
                Description = request.description,
                Price = request.price,
                Category = request.category,
                Discount = request.discount,
                ImageUrl = request.imageUrl
            };

            await _productCommand.AddProduct(product);
            return await BuildProductResponse(product);
        }

        public async Task<ProductResponse> DeleteProduct(Guid productId)
        {
            await _productServiceExtensions.ValidateProductExist(productId);
            await _productServiceExtensions.ValidateProductHasSaleHistory(productId);

            var product = await _productQuery.GetProductById(productId);

            await _productCommand.DeleteProduct(product);

            return await BuildProductResponse(product);
        }

        public async Task<ProductResponse> GetProductById(Guid productId)
        {
            await _productServiceExtensions.ValidateProductExist(productId);

            var product = await _productQuery.GetProductById(productId);
 
            return await BuildProductResponse(product);
        }

        public async Task<ProductResponse> UpdateProduct(Guid id, ProductRequest request)
        {
            await _productServiceExtensions.ValidateProductExist(id);
            await _productServiceExtensions.ValidateNameUpdate(id, request);
            await _productServiceExtensions.ValidateProductRequest(request);
            
            var product = await _productQuery.GetProductById(id);   

            product.Name = request.name;
            product.Description = request.description;
            product.Price = request.price;
            product.Discount = request.discount;
            product.ImageUrl = request.imageUrl;
            product.Category = request.category;

            await _productCommand.UpdateProduct(product);
            return await BuildProductResponse(product);
        }

        private async Task<ProductResponse> BuildProductResponse(Product product)
        {
            return new ProductResponse
            {
                id = product.ProductId,
                name = product.Name,
                description = product.Description,
                price = product.Price,
                discount = product.Discount,
                imageUrl = product.ImageUrl,
                category = await _categoryService.GetProductCategory(product.Category)
            };
        }
    }


}
