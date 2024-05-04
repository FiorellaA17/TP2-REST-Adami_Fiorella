
namespace Application.ErrorHandler
{
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException(string productName)
            : base($"Producto con el nombre '{productName}' ya existe.")
        {
        }
    }
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid productId)
            : base($"Producto con el ID {productId} no se encontró.")
        {
        }

        public IEnumerable<Guid> ProductIds { get; private set; }

        public ProductNotFoundException(IEnumerable<Guid> productIds)
            : base($"Productos con IDs {string.Join(", ", productIds)} no se encontró.")
        {
            ProductIds = productIds;
        }
    }
    public class ProductHasSalesHistoryException : Exception
    {
        public ProductHasSalesHistoryException(Guid productId)
            : base($"Se intentó eliminar un producto con historial de ventas con ID: {productId}.")
        {
        }
    }
    public class CategoryDoesNotExistException : Exception
    {

        public CategoryDoesNotExistException(int id)
            : base($" No existe categoria con ID '{id}'.")
        {
        }
        public List<int>? MissingCategoryIds { get; private set; }

        public CategoryDoesNotExistException(List<int> ids)
            : base($"Las siguientes categorías no existen: {string.Join(", ", ids)}")
        {
            MissingCategoryIds = ids;
        }
    }

    public class PaymentMismatchException : Exception
    {
        public decimal ExpectedTotal { get; private set; }
        public decimal ReceivedTotal { get; private set; }

        public PaymentMismatchException(decimal expectedTotal, decimal receivedTotal)
            : base($"El total pagado no coincide con el total calculado. Esperado: {expectedTotal}, Recibido: {receivedTotal}")
        {
            ExpectedTotal = expectedTotal;
            ReceivedTotal = receivedTotal;
        }
    }

    public class NoProductsProvidedException : Exception
    {
        public NoProductsProvidedException()
            : base("La solicitud de creación de venta no contiene productos.")
        {
        }
    }

    public class SaleNotFoundException : Exception
    {
        public SaleNotFoundException(int id)
            : base($"No se encontró ventas con el ID {id}.")
        {
        }
    }

    public class DiscountException : Exception
    {
        public DiscountException(int id)
            : base($" El valor {id} no es válido. El descuento debe estar entre 0 y 100.")
        {
        }
    }
}