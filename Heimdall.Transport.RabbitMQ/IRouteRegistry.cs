using System;
using Heimdal.Transport.Interfaces;

namespace Heimdall.Transport.RabbitMQ
{
    /// <summary>
    /// Determines where to route messages
    /// </summary>
    public interface IRouteRegistry
    {
        Uri For<T>() where T:IMessage;
    }
}