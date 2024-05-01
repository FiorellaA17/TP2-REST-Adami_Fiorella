

namespace Application.Models
{
    public class ApiError
    {
        public string message { get; set; }
        public ApiError(string Message)
        {
            message = Message;
        }
    }
}
