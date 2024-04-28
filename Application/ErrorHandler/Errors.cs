
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
}
