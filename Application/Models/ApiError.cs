﻿

namespace Application.Models
{
    public class ApiError
    {
        public string Message { get; set; }

        public ApiError(string message)
        {
            Message = message;
        }
    }
}
