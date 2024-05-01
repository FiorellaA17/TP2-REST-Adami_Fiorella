

namespace Application.Models
{
    public class ApiError
    {
        public string message { get; set; }
        public ApiError(string message)
        {
            message = message;
        }
    }
}
