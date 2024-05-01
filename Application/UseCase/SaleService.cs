using Application.Interfaces;
using Application.Models;
using Domain.Entities;


namespace Application.UseCase
{
    public class SaleService : ISaleService
    {
        private readonly ISaleCommand _saleCommand;
        private readonly ISaleQuery _saleQuery;
        private readonly IProductQuery _productQuery;

        public SaleService(ISaleCommand saleCommand, ISaleQuery saleQuery, IProductQuery productQuery)
        {
            _saleCommand = saleCommand;
            _saleQuery = saleQuery;
            _productQuery = productQuery;
        }

        public async Task<SaleResponse> GenerateSale(SaleRequest request)
        {
            if (request.products == null || !request.products.Any())
            {
                //_logger.LogError("GenerateSaleAsync called with no products.");
                throw new ArgumentException("No products provided.");
            }

            var sale = new Sale
            {
                Date = DateTime.Now,
                SaleProducts = new List<SaleProduct>(),
                TotalPay = request.totalPayed 
            };

            decimal subtotal = 0;
            decimal totalDiscount = 0;

            foreach (var productRequest in request.products)
            {
                // Intentar convertir el string ProductId a un Guid
                if (!Guid.TryParse(productRequest.productId, out Guid productId))
                {
                    // Aquí puedes registrar un error o manejar la situación según la necesidad
                    //_logger.LogError("Invalid GUID for product ID: {ProductId}", productRequest.ProductId);
                    continue; // Saltar esta iteración si el GUID no es válido
                }
                var product = await _productQuery.GetProductById(productId);
                if (product != null)
                {
                    decimal discountedPrice = product.Price * (1 - product.Discount / 100.0m);
                    subtotal += product.Price * productRequest.quantity;
                    totalDiscount += (product.Price - discountedPrice) * productRequest.quantity;

                    sale.SaleProducts.Add(new SaleProduct
                    {
                        Product = product.ProductId,
                        Quantity = productRequest.quantity,
                        Price = product.Price,
                        Discount = product.Discount
                    });
                }
            }

            sale.Subtotal = subtotal;
            sale.TotalDiscount = totalDiscount;
            sale.Taxes = 1.21m;
            sale.TotalPay = (subtotal - totalDiscount) * sale.Taxes;

            await _saleCommand.AddSale(sale);

            return new SaleResponse
            {
                id = sale.SaleId,
                totalPay = sale.TotalPay,
                totalQuantity = sale.SaleProducts.Sum(p => p.Quantity),
                subtotal = sale.Subtotal,
                totalDiscount = sale.TotalDiscount,
                taxes = sale.Taxes,
                date = sale.Date,
                products = sale.SaleProducts.Select(sp => new SaleProductResponse
                {
                    id = sp.ShoppingCartId,
                    productId = sp.Product.ToString(),
                    quantity = sp.Quantity,
                    price = sp.Price,
                    discount = sp.Discount
                }).ToList()
            };
        }

        public async Task<IEnumerable<SaleGetResponse>> GetSales(DateTime? from, DateTime? to)
        {
            var sales = await _saleQuery.GetSales(from, to);
            return sales.Select(s => new SaleGetResponse
            {
                id = s.SaleId,
                totalPay = s.TotalPay,
                totalQuantity = s.SaleProducts.Sum(sp => sp.Quantity),
                date = s.Date
            });
        }
    }
}
