using Application.ErrorHandler;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCase.Service.Extensions
{
    public class ProductServiceExtensions : IProductServiceExtensions
    {
        private readonly IProductQuery _productQuery;
        private readonly ICategoryService _categoryService;

        public ProductServiceExtensions(IProductQuery productQuery, ICategoryService categoryService)
        {
            _productQuery = productQuery;
            _categoryService = categoryService;
        }

        public IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductFilter filter)
        {
            if (filter.categories != null && filter.categories.Any())
            {
                query = query.Where(p => filter.categories.Contains(p.CategoryName.CategoryId));
            }

            if (!string.IsNullOrEmpty(filter.name))
            {
                query = query.Where(p => p.Name.Contains(filter.name));
            }

            if (filter.offset > 0)
            {
                query = query.Skip(filter.offset);
            }

            if (filter.limit > 0)
            {
                query = query.Take(filter.limit);
            }

            return query;
        }

        public async Task ValidateProductRequest(ProductRequest request)
        {
            List<string> errorMessages = new List<string>();

            if (request.price <= 0)
            {
                errorMessages.Add("El precio debe ser mayor a cero.");
            }

            if (request.discount < 0 || request.discount > 100)
            {
                errorMessages.Add("El descuento debe estar entre 0% y 100%.");
            }

            if (!await _categoryService.CategoryExists(request.category))
            {
                errorMessages.Add($"La categoría '{request.category}' no existe.");
            }

            if (errorMessages.Count > 0)
            {
                throw new BadRequestException(string.Join(" ", errorMessages));
            }
        }

        public async Task ValidateProductDoesNotExistByName(ProductRequest request)
        {
            if (await _productQuery.ProductExistsByName(request.name))
            {
                throw new ProductAlreadyExistsException(request.name);
            }
        }

        public async Task ValidateNameUpdate(Guid id, ProductRequest request)
        {
            var product = await _productQuery.GetProductById(id);

            if (product.Name != request.name && await _productQuery.ProductExistsByName(request.name))
            {
                throw new ProductAlreadyExistsException(request.name);
            }
        }
        public async Task ValidateProductExist(Guid id)
        {
            var queryProduct = await _productQuery.GetProductById(id);
            if (queryProduct == null)
            {
                throw new ProductNotFoundException(id);
            }
        }

        public async Task ValidateProductHasSaleHistory(Guid id)
        {
            var product = await _productQuery.GetProductById(id);

            if (product.SaleProducts.Any())
            {
                throw new ProductHasSalesHistoryException(id);
            }
        }
    }
}
