using System;
using System.Threading;
using System.Threading.Tasks;
using Heimdal.Transport.Interfaces;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Heimdall.Transport.RabbitMQ
{
    /// <summary>
    /// Gateway for communicating throughout the Heimdall system using RabbitMQ
    /// </summary>
    public class RabbitMqGateway : IHeimdallGateway
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRouteRegistry _routeRegistry;

        public RabbitMqGateway(ISendEndpointProvider sendEndpointProvider,
            IPublishEndpoint publishEndpoint, IRouteRegistry routeRegistry)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
            _routeRegistry = routeRegistry;
        }

        public async Task Publish<T>(T @event, CancellationToken cancellationToken = default(CancellationToken)) where T : class, IEvent
        {
            await _publishEndpoint.Publish(@event, cancellationToken);
        }

        public async Task Send<T>(T message, CancellationToken cancellationToken = default(CancellationToken)) where T : class, ICommand
        {
            var destinationQ = _routeRegistry.For<T>();
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(destinationQ);
            await sendEndpoint.Send(message, cancellationToken);
        }
    }
}