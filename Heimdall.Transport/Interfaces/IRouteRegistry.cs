using System;

namespace Heimdall.Transport.Interfaces
{
    /// <summary>
    /// Determines where to route messages
    /// </summary>
    public interface IRouteRegistry
    {
        Uri For<T>() where T:IMessage;
    }
}