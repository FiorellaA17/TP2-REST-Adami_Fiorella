
namespace Application.ErrorHandler
{
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException(string productName)
            : base($"Producto con el nombre '{productName}' ya existe.")
        {
        }
    }
}
