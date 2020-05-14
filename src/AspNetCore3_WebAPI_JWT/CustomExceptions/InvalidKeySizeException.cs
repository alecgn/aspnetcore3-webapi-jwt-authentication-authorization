using System;

namespace AspNetCore3_WebAPI_JWT.CustomExceptions
{
    public class InvalidKeySizeException : Exception
    {
        public InvalidKeySizeException()
        {

        }

        public InvalidKeySizeException(string message)
            : base(message)
        {

        }

        public InvalidKeySizeException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}
