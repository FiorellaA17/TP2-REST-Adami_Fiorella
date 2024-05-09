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
            var sale = new Sale
            {
                Date = DateTime.Now,
                SaleProducts = new List<SaleProduct>(),
            };

            List<string> errors = new List<string>();

            await CalculateSale(request, sale, errors);

            if (sale.TotalPay != request.totalPayed)
            {
                errors.Add($"El total calculado ({sale.TotalPay}) no coincide con el total pagado ({request.totalPayed}).");
            }

            if (errors.Any())
            {
                throw new BadRequestException(string.Join(" ", errors));
            }

            await _saleCommand.AddSale(sale);

            return BuildSaleResponse(sale);
        }

        public async Task CalculateSale(SaleRequest request, Sale sale, List<string> errors)
        {
            decimal subtotal = 0;
            decimal totalDiscount = 0;

            foreach (var productRequest in request.products)
            {
                if (productRequest.quantity <= 0)
                {
                    errors.Add($"La cantidad para el producto con ID {productRequest.productId} debe ser mayor a cero.");
                    continue;
                }

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
                    errors.Add($"Producto con ID {productRequest.productId} no encontrado.");
                }
            }

            sale.Subtotal = subtotal;
            sale.TotalDiscount = totalDiscount;
            sale.Taxes = 1.21m;
            sale.TotalPay = Math.Round(((subtotal - totalDiscount) * sale.Taxes), 2);
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
