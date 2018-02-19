using System;

namespace ElearnerApi.Data.Models.ExceptionHandling
{
    public class ErrorMessage
    {
        public String Message { get; set; }

        public ErrorMessage(string message)
        {
            Message = message;
        }

        public ErrorMessage(Exception exception)
        {
            Message = exception.Message;
        }
    }
}