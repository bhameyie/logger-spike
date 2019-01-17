using Autofac;
using Heimdal.Transport;
using Heimdal.Transport.Interfaces;
using MassTransit;

namespace Heimdall.Transport.RabbitMQ
{
    public class RabbitMqConfigurationAgent : IConfigurationAgent
    {
        private IRouteRegistry _routeRegistry;

        public RabbitMqConfigurationAgent WithRegistry(IRouteRegistry routeRegistry)
        {
            _routeRegistry = routeRegistry;
            return this;
        }

        public void Configure(TransportConfigurator configurator)
        {
            configurator.Container.Register(c =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(sbc =>
                        sbc.Host(configurator.Configuration["RabbitMQ:Host"], configurator.Configuration["RabbitMQ:VHost"], h =>
                        {
                            h.Username(configurator.Configuration["RabbitMQ:User"]);
                            h.Password(configurator.Configuration["RabbitMQ:Password"]);
                        })
                    );
                })
                .As<IBusControl>()
                .As<IPublishEndpoint>()
                .As<ISendEndpointProvider>()
                .As<IBus>()
                .SingleInstance();

            configurator.Container.RegisterModule(new RabbitModule(_routeRegistry));
        }

        public void Release(IConfiguredTransport transport)
        {

        }
    }
}
