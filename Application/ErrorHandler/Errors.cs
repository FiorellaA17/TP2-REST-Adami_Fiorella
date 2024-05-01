
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
        //public ProductNotFoundException(string ex)
        //   : base(ex)
        //{
        //}
    }
    public class CheckProductDeletionException : Exception
    {
        public CheckProductDeletionException(Guid productId)
            : base($"Se intentó eliminar un producto con historial de ventas con ID: {productId}.")
        {
        }
        //public CheckProductDeletionException(string ex)
        //    : base(ex)
        //{
        //}
    }
}
