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

        public async Task<ProductResponse> CreateProduct(CreateProductDto productDto)
        {
            if (await _productQuery.ProductExistsByName(productDto.Name))
            {
                throw new ProductAlreadyExistsException(productDto.Name);
            }
            try
            {
                var productId = Guid.NewGuid();
                var product = new Product
                {
                    ProductId = productId,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Category = productDto.Category,
                    Discount = productDto.Discount,
                    ImageUrl = productDto.ImageUrl
                };
                await _productCommand.AddProduct(product);

                return new ProductResponse
                {
                    ProductId = productId,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Discount = productDto.Discount,
                    //Category = productDto.Category,
                    ImageUrl = productDto.ImageUrl,
                    Category = new ProductCategoryResponse
                    {
                        Name = await _productQuery.GetCategoryNameById(productDto.Category),
                    },
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
            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                query = query.Where(p => filter.CategoryIds.Contains(p.CategoryName.CategoryId));
            }

            // Aplica filtro por nombre solo si se proporciona un nombre.
            if (!string.IsNullOrEmpty(filter.NameFilter))
            {
                query = query.Where(p => p.Name.Contains(filter.NameFilter));
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
                CategoryName = p.CategoryName.Name
            }).ToListAsync();

            return products;

            //query = query.Skip(filter.Offset).Take(filter.Limit);

            //var products = await query.ToListAsync();
            //return products.Select(MapToProductGetResponse);
        }

        //private ProductGetResponse MapToProductGetResponse(Product product)
        //{
        //    return new ProductGetResponse
        //    {
        //        Id = product.ProductId.ToString(),
        //        Name = product.Name,
        //        Price = product.Price,
        //        Discount = product.Discount,
        //        ImageUrl = product.ImageUrl,
        //        CategoryName = product.CategoryName.Name 
        //    };
        //}
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
