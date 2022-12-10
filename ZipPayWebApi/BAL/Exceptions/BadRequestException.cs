using System;

namespace ZipPayWebApp.BAL.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(ErrorCodes errorCode,string message) : base(message)
        {
            HResult = (int) errorCode;
        }
    }
}
