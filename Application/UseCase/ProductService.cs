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
        private readonly ICategoryService _categoryService;

        public ProductService(IProductQuery productQuery, IProductCommand productCommand, ICategoryService categoryService)
        {
            _productQuery = productQuery;
            _productCommand = productCommand;
            _categoryService = categoryService;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            if (await _productQuery.ProductExistsByName(request.name))
            {
                throw new ProductAlreadyExistsException(request.name);
            }
            try
            {
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

            catch (Exception ex)
            {
                Console.WriteLine($"Error creando el producto: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductResponse> DeleteProduct(Guid productId)
        {
            var product = await _productQuery.GetProductById(productId);
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            await _productCommand.DeleteProduct(product);
            return await BuildProductResponse(product);

            //return new ProductResponse
            //{
            //    Id = product.ProductId.ToString(),
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    Discount = product.Discount,
            //    ImageUrl = product.ImageUrl,
            //    Category = await _categoryService.GetProductCategory(product.Category)
            //    //Category = new ProductCategory
            //    //{
            //    //    Id = product.Category,
            //    //    //Name = product.CategoryName.Name
            //    //    Name = await _productQuery.GetCategoryNameById(product.Category),
            //    //}
            //};
        }

        public async Task<IEnumerable<ProductGetResponse>> GetFilteredProducts(ProductFilter filter)
        {

            var query = _productQuery.GetListProducts();
            // Aplica filtro por categoría si Categories no es nulo y tiene elementos.
            if (filter.categories != null && filter.categories.Any())
            {
                query = query.Where(p => filter.categories.Contains(p.CategoryName.CategoryId));
            }

            // Aplica filtro por nombre solo si se proporciona un nombre.
            if (!string.IsNullOrEmpty(filter.name))
            {
                query = query.Where(p => p.Name.Contains(filter.name));
            }
            var products = await query
            .Skip(filter.offset)
            .Take(filter.limit)
            .Select(p => new ProductGetResponse
            {
                id = p.ProductId.ToString(),
                name = p.Name,
                price = p.Price,
                discount = p.Discount,
                imageUrl = p.ImageUrl,
                categoryName = p.Category
            }).ToListAsync();

            return products;
        }

        public async Task<ProductResponse> GetProductById(Guid productId)
        {
            var product = await _productQuery.GetProductById(productId);
            if (product == null)
            {
                return null;
            }
            return await BuildProductResponse(product);
        }

        public async Task<ProductResponse> UpdateProduct(Guid id, ProductRequest request)
        {
            var product = await _productQuery.GetProductById(id);

            // Verifica si el nombre del producto ha cambiado y si el nuevo nombre ya existe.
            if (product.Name != request.name && await _productQuery.ProductExistsByName(request.name))
            {
                throw new ProductAlreadyExistsException(request.name);
            }

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
                id = product.ProductId.ToString(),
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
