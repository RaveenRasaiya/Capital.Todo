using System;

namespace Capital.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException()
        {

        }
        public ApiException(string message) : base(message)
        {

        }

        public ApiException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
