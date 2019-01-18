using System;
using Autofac;

namespace Heimdal.Transport.Interfaces
{
    public interface IConfiguredTransport : IDisposable
    {
        IContainer BuiltContainer { get; }
        IHeimdallGateway Gateway { get; }
    }
}