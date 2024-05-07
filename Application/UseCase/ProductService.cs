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

        public async Task<IEnumerable<ProductGetResponse>> GetFilteredProducts(ProductFilter filter)
        {
            var invalidCategories = new List<int>();

            if (filter.categories != null)
            { 
                foreach (var category in filter.categories)
                {
                    bool exists = await _categoryService.CategoryExist(category);
                    if (!exists)
                    {
                        invalidCategories.Add(category);
                    }
                }
                if (invalidCategories.Any())
                {
                    throw new CategoryDoesNotExistException(invalidCategories);
                }
            }         

            var query = _productQuery.GetListProducts();

            if (filter.categories != null && filter.categories.Any())
            {
                query = query.Where(p => filter.categories.Contains(p.CategoryName.CategoryId));
            }

            if (!string.IsNullOrEmpty(filter.name))
            {
                query = query.Where(p => p.Name.Contains(filter.name));
            }

            var products = await query
            .Skip(filter.offset)
            .Take(filter.limit)
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

            if (await _productQuery.ProductExistsByName(request.name))
            {
                throw new ProductAlreadyExistsException(request.name);
            }

            if(request.category != null)
            {
                await _categoryService.EnsureCategoryExists(request.category);
            }
    
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
            var product = await _productQuery.GetProductById(productId);

            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            if (product.SaleProducts.Any())
            {
                throw new ProductHasSalesHistoryException(productId);
            }

            await _productCommand.DeleteProduct(product);
            return await BuildProductResponse(product);
        }

        public async Task<ProductResponse> GetProductById(Guid productId)
        {
            var product = await _productQuery.GetProductById(productId);
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }
            return await BuildProductResponse(product);
        }

        public async Task<ProductResponse> UpdateProduct(Guid id, ProductRequest request)
        {
            var product = await _productQuery.GetProductById(id);
            if(product == null)
            {
                throw new ProductNotFoundException(id);
            }

            if (request.category != null)
            {
                await _categoryService.EnsureCategoryExists(request.category);
            }
  
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
