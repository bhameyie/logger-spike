using System;
using System.Threading;
using System.Threading.Tasks;
using Heimdal.Transport.Interfaces;

namespace Heimdall.Transport.RabbitMQ
{
    public class RabbitMqGateway : IHeimdallGateway {
        public Task Publish<T>(T @event, CancellationToken cancellationToken = default(CancellationToken)) where T : IEvent
        {
            throw new NotImplementedException();
        }

        public Task Send<T>(T message, CancellationToken cancellationToken = default(CancellationToken)) where T : ICommand
        {
            throw new NotImplementedException();
        }
    }
}