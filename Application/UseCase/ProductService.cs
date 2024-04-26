using Application.ErrorHandler;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCase
{
    public class ProductService : IProductService
    {
        private readonly IProductQuery _productQuery;
        private readonly IProductCommand _productCommand;

        public ProductService(IProductQuery productQuery, IProductCommand productCommand)
        {
            _productQuery = productQuery;
            _productCommand = productCommand;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            if (await _productQuery.ProductExistsByName(request.Name))
            {
                throw new ProductAlreadyExistsException(request.Name);
            }
            try
            {
                var product = new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Category = request.Category,
                    Discount = request.Discount,
                    ImageUrl = request.ImageUrl
                };
                await _productCommand.AddProduct(product);

                return new ProductResponse
                {
                    Id = product.ProductId.ToString(),
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Discount = request.Discount,
                    ImageUrl = request.ImageUrl,
                    Category = new ProductCategory
                    {
                        Id = request.Category,
                        Name = await _productQuery.GetCategoryNameById(request.Category),
                    }
                };
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<ProductGetResponse>> GetFilteredProducts(ProductFilter filter)
        {

            var query = _productQuery.GetListProducts();
            // Aplica filtro por categoría si Categories no es nulo y tiene elementos.
            if (filter.Categories != null && filter.Categories.Any())
            {
                query = query.Where(p => filter.Categories.Contains(p.CategoryName.CategoryId));
            }

            // Aplica filtro por nombre solo si se proporciona un nombre.
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(p => p.Name.Contains(filter.Name));
            }
            var products = await query
            .Skip(filter.Offset)
            .Take(filter.Limit)
            .Select(p => new ProductGetResponse
            {
                Id = p.ProductId.ToString(),
                Name = p.Name,
                Price = p.Price,
                Discount = p.Discount,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category
            }).ToListAsync();

            return products;
        }

        public async Task<ProductDto> GetProductById(Guid productId)
        {
            var product = await _productQuery.GetProductById(productId);
            //return new ProductDto { ProductId = productId,
            //};
            //return new ProductResponse
            //{
            //    Category = new ProductCategoryResponse
            //    {
            //        Name = product.CategoryName.Name,
            //    },
            //    Description = product.Description,
            //    Price = product.Price,
            //    Name = product.Name,
            //    ProductId = product.ProductId,
            //    Discount = product.Discount,
            //};
            return MapProductEntityToDto(product);
        }

        private ProductDto MapProductEntityToDto(Product productEntity)
        {
            return new ProductDto
            {
                ProductId = productEntity.ProductId,
                Name = productEntity.Name,
                Description = productEntity.Description,
                Price = productEntity.Price,
                Category = productEntity.Category,
                Discount = productEntity.Discount,
                ImageUrl = productEntity.ImageUrl,
            };
        }

        
    }


}
