using System;

namespace Heimdall.Transport
{
    public class GatewayException : Exception
    {
        public GatewayException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
