
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
       
    }
    public class ProductHasSalesHistoryException : Exception
    {
        public ProductHasSalesHistoryException(Guid productId)
            : base($"Se intentó eliminar un producto con historial de ventas con ID: {productId}.")
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

    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}