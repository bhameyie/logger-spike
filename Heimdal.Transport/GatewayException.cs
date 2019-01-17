using System;

namespace Heimdal.Transport
{
    public class GatewayException : Exception
    {
        public GatewayException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
