using System;
using System.IO;
using System.Reflection;
using Autofac;
using Heimdal.Transport;
using Heimdal.Transport.Interfaces;
using Heimdall.Transport.RabbitMQ;
using log4net;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Heimdall.Overseer
{
    public class ServiceInitiator :IDisposable
    {
        private readonly IConfiguredTransport _configuredTransport;
        private readonly ILog _logger;

        public ServiceInitiator ()
        {
            _logger = LogManager.GetLogger(typeof(ServiceInitiator));
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.{env.EnvironmentName}.json")
                .Build();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(config).As<IConfiguration>();

            containerBuilder.RegisterConsumers(Assembly.GetExecutingAssembly());


            _configuredTransport= new TransportConfigurator(containerBuilder, config)
                .WithAgent<RabbitMqConfigurationAgent>()
                .Configure();
            
            _configuredTransport.BuiltContainer.Resolve<IBusControl>().Start();
            
            _logger.Info("Started the transport");
            
        }

        public void Dispose()
        {
            _configuredTransport?.Dispose();
            
            _logger.Info("Transport disposed");
        }
    }
}