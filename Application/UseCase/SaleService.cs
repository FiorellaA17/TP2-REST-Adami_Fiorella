using Application.ErrorHandler;
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
                throw new NoProductsProvidedException();
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
                var product = await _productQuery.GetProductById(productRequest.productId);
                if (product != null)
                {
                    decimal discountedPrice = product.Price * (1 - product.Discount / 100.0m);
                    subtotal += product.Price * productRequest.quantity;
                    totalDiscount += Math.Round((product.Price - discountedPrice) * productRequest.quantity, 2);

                    sale.SaleProducts.Add(new SaleProduct
                    {
                        Product = product.ProductId,
                        Quantity = productRequest.quantity,
                        Price = product.Price,
                        Discount = product.Discount
                    });
                }
                else
                {
                    throw new ProductNotFoundException(new[] { productRequest.productId});
                }
            }

            sale.Subtotal = subtotal;
            sale.TotalDiscount = totalDiscount;
            sale.Taxes = 1.21m;
            sale.TotalPay = Math.Round(((subtotal - totalDiscount) * sale.Taxes), 2);

            if (sale.TotalPay != request.totalPayed)
            {
                throw new PaymentMismatchException(sale.TotalPay, request.totalPayed);
            }

            await _saleCommand.AddSale(sale);

            return BuildSaleResponse(sale);
        }

        public async Task<SaleResponse> GetSaleById(int id)
        {
            var sale = await _saleQuery.GetSaleById(id);
            if (sale == null)
            {
                throw new SaleNotFoundException(id);
            }

            return BuildSaleResponse(sale);
        }

        public async Task<IEnumerable<SaleGetResponse>> GetSales(DateTime? from, DateTime? to)
        {
            var sales = await _saleQuery.GetSalesFromTo(from, to);
            return sales.Select(s => BuildSaleGetResponse(s));
        }

        private SaleGetResponse BuildSaleGetResponse(Sale sale)
        {
            return new SaleGetResponse
            {
                id = sale.SaleId,
                totalPay = sale.TotalPay,
                totalQuantity = sale.SaleProducts.Sum(sp => sp.Quantity),
                date = sale.Date
            };
        }

        private SaleResponse BuildSaleResponse(Sale sale)
        {
            return new SaleResponse
            {
                id = sale.SaleId,
                totalPay = sale.TotalPay,
                totalQuantity = sale.SaleProducts.Sum(sp => sp.Quantity),
                subtotal = sale.Subtotal,
                totalDiscount = sale.TotalDiscount,
                taxes = sale.Taxes,
                date = sale.Date,
                products = sale.SaleProducts.Select(sp => new SaleProductResponse
                {
                    id = sp.ShoppingCartId,
                    productId = sp.Product,
                    quantity = sp.Quantity,
                    price = sp.Price,
                    discount = sp.Discount
                }).ToList()
            };
        }
    }
}
