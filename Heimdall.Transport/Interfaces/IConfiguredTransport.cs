using System;
using Autofac;

namespace Heimdall.Transport.Interfaces
{
    public interface IConfiguredTransport : IDisposable
    {
        IContainer BuiltContainer { get; }
        IHeimdallGateway Gateway { get; }
        void Start();
    }
}