using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Heimdall.Transport.Interfaces;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Heimdall.Transport.RabbitMQ
{
    public class RabbitMqConfigurationAgent : IConfigurationAgent
    {
        private IRouteRegistry _routeRegistry;
        private readonly List< Action<IRabbitMqReceiveEndpointConfigurator,IComponentContext> > _consumers;

        public RabbitMqConfigurationAgent()
        {
            _consumers = new List<Action<IRabbitMqReceiveEndpointConfigurator, IComponentContext>>();
        }

        public RabbitMqConfigurationAgent WithRegistry(IRouteRegistry routeRegistry)
        {
            _routeRegistry = routeRegistry;
            return this;
        }
        
       // public RabbitMqConfigurationAgent With

        public void Configure(TransportConfigurator configurator)
        {
            configurator.Container.Register(container =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(sbc =>
                        {
                          var host=  sbc.Host(configurator.Configuration["RabbitMQ:Host"],
                                configurator.Configuration["RabbitMQ:VHost"], h =>
                                {
                                    h.Username(configurator.Configuration["RabbitMQ:User"]);
                                    h.Password(configurator.Configuration["RabbitMQ:Password"]);
                                });

                            if (_consumers.Any())
                            {
                                sbc.ReceiveEndpoint(host, configurator.Configuration["Heimdall:ServiceQ"],
                                    receiveConf =>
                                    {
                                        _consumers.ForEach(act=>act(receiveConf,container));
                                    });
                            }
                        }
                    );
                })
                .As<IBusControl>()
                .As<IPublishEndpoint>()
                .As<ISendEndpointProvider>()
                .As<IBus>()
                .SingleInstance();

            configurator.Container.RegisterModule(new RabbitModule(_routeRegistry));
        }

        public RabbitMqConfigurationAgent WithConsumer<T>() where T:IConsumer
        {
            Action<IRabbitMqReceiveEndpointConfigurator,IComponentContext> a = (receiveConf, container) =>
            {
                receiveConf.Consumer(typeof(T), container.Resolve);
            };

            _consumers.Add(a);
            return this;
        }

        public void OnRelease(IConfiguredTransport transport)
        {
            transport.BuiltContainer.Resolve<IBusControl>().Stop();
        }

        public void OnStart(IConfiguredTransport transport)
        {
            transport.BuiltContainer.Resolve<IBusControl>().Start();
        }
    }
}
